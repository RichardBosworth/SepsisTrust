using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Guidelines.Model;

namespace Guidelines.IO
{
    public class XmlFileGuidelineRetriever : IGuidelineRetriever
    {
        public async Task<Guideline> RetrieveGuidelineAsync( string identifier )
        {
            // Load the guideline element data.
            /*var mobileServiceClient = new MobileServiceClient("http://sepsis.azurewebsites.net");
            var guidelineTable = mobileServiceClient.GetTable("Guideline");
            var mainGuideline = await guidelineTable.LookupAsync("6b75729b504648e795cbef6dd75e0398");
            var guideLineElement = XElement.Parse((string) mainGuideline["guidelineContent"]);*/
            var guideLineElement = XElement.Load("TestGuideline.xml");


            // Retrieve the data from that guideline elements.
            var guideline = new Guideline();

            // Use reflection to add common properties.
            AddCommonProperties(guideLineElement, guideline);

            // Add phases.
            var phaseElements = guideLineElement.Descendants("Phase");
            foreach ( var phaseElement in phaseElements )
            {
                ParsePhase(guideline, phaseElement);
            }

            // Identify initial phase, and set this as the phase.
            var entryPhaseIdentifier = guideLineElement.Attribute("EntryPhaseId")?.Value;
            var entryPhase = guideline.Phases.FirstOrDefault(phase => string.Compare(phase.Identifier, entryPhaseIdentifier, StringComparison.OrdinalIgnoreCase) == 0);
            guideline.EntryPhase = entryPhase ?? guideline.Phases.FirstOrDefault();

            return guideline;
        }

        private static void ParsePhase( Guideline guideline, XElement phaseElement )
        {
            // Generate a new phase for the element.
            var phase = new Phase(guideline);

            // Add the common properties.
            AddCommonProperties(phaseElement, phase);

            // Go through the blocks contained in this phase.
            foreach ( var blockElement in phaseElement.Descendants("Block") )
            {
                ParseBlockElement(blockElement, phase, guideline);
            }

            // Add the entry block to the phase.
            var entryBlockIdentifier = phaseElement.Attribute("EntryBlockId")?.Value;
            var identifiedBlock = phase.Blocks.FirstOrDefault(block => string.Compare(block.Identifier, entryBlockIdentifier, StringComparison.CurrentCultureIgnoreCase) == 0);
            phase.EntryBlock = identifiedBlock ?? phase.Blocks.First();

            // Add the phase to the guideline.
            guideline.Phases.Add(phase);
            guideline.LinkManager.Register(phase);
        }

        private static void ParseBlockElement( XElement blockElement, Phase phase, Guideline guideline )
        {
            // Generate the appropriate block.
            var block = GenerateBlockOfAppropriateType(blockElement, phase);

            // Add the common properties for this block.
            if ( block is SummaryBlock )
            {
                AddCommonProperties(blockElement, block as SummaryBlock);
            }
            else
            {
                AddCommonProperties(blockElement, block);
            }

            // If block is assessment block, add assessment type.
            if ( block is AssessmentBlock )
            {
                var assessmentTypeString = blockElement.Attribute("AssessmentType")?.Value;
                AssessmentType assessmentType;
                var successful = Enum.TryParse(assessmentTypeString, out assessmentType);
                if ( successful )
                {
                    ( (AssessmentBlock) block ).AssessmentType = assessmentType;
                }
            }

            // Add block links.
            var linksElement = blockElement.Descendants("Links").FirstOrDefault();
            if ( linksElement != null )
            {
                foreach ( var linkElement in linksElement.Descendants("Link") )
                {
                    ParseLinkElement(linkElement, block);
                }
            }

            // Add block activities.
            var activitiesElement = blockElement.Descendants("Activities").FirstOrDefault();
            if ( activitiesElement != null )
            {
                foreach ( var activityElement in activitiesElement.Descendants("Activity") )
                {
                    // Generate the BlockActivityData for this element.
                    var blockActivityData = new BlockActivityData();

                    // Generate the common properties for this BlockActivityData.
                    AddCommonProperties(activityElement, blockActivityData);

                    // Add this activity data to the block.
                    block.BlockActivities.Add(blockActivityData);
                }
            }

            // Add this block to the phase.
            phase.Blocks.Add(block);
            guideline.LinkManager.Register(block);
        }

        private static Block GenerateBlockOfAppropriateType( XElement blockElement, Phase phase )
        {
            // Identify the type of block that this is, and generate the relevant block.
            var blockTypeIdentifier = blockElement.Attribute("Type")?.Value;
            var blockTypes = new Dictionary<string, Type>
                             {
                                 {"Summary", typeof(SummaryBlock)},
                                 {"Assessment", typeof(AssessmentBlock)},
                                 {"Action", typeof(ActionBlock)}
                             };
            var blockType = blockTypes[blockTypeIdentifier] ?? typeof(SummaryBlock);
            var block = (Block) Activator.CreateInstance(blockType, phase);
            return block;
        }

        private static void ParseLinkElement( XElement linkElement, Block block )
        {
            // Obtain the text data from the attributes.
            var minimumScoreString = linkElement.Attribute("MinimumScoreToActivate")?.Value;
            var maximumScoreString = linkElement.Attribute("MaximumScoreToActivate")?.Value;
            var linkedEntityIdentifier = linkElement.Attribute("LinkedGuidelineEntityIdentifier")?.Value;

            // Check if an entity identifier has been specified.
            // If one has not been specified, then the link is no use,
            // so it will be ignored.
            if ( !string.IsNullOrWhiteSpace(linkedEntityIdentifier) )
            {
                // Convert the score data from string representation to double values.
                var minimumScore = double.Parse(minimumScoreString);
                var maximumScore = double.Parse(maximumScoreString);

                // Create the new link, and add it to the block.
                var link = new Link(minimumScore, maximumScore, linkedEntityIdentifier);
                block.Links.Add(link);
            }
        }

        /// <summary>
        ///     Uses reflection to compare the properties of a guideline object to attributes found in the specified XML element.
        /// </summary>
        /// <typeparam name="T">The type of the guideline object. Properties will be searched from this type.</typeparam>
        /// <param name="xmlElement">
        ///     The XML element that contains the attributes containing data matching the properties in the
        ///     specified type.
        /// </param>
        /// <param name="obj">
        ///     The instance of the guideline object. Data found in the XML attributes will be set on the relevant
        ///     properties on this object.
        /// </param>
        private static void AddCommonProperties<T>( XElement xmlElement, T obj )
        {
            foreach ( var property in typeof(T).GetRuntimeProperties() )
            {
                var value = xmlElement.Attribute(property.Name)?.Value;

                if ( value != null )
                {
                    if ( property.PropertyType.GetTypeInfo().IsEnum )
                    {
                        var propertyEnumValue = Enum.Parse(property.PropertyType, value);
                        property.SetValue(obj, propertyEnumValue);
                    }
                    else
                    {
                        property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
                    }
                }
            }
        }
    }
}