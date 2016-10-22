using System;
using System.Collections.Generic;
using Guidelines.Model;
using SepsisTrust.GuidelineUI.Views;
using Xamarin.Forms;

namespace SepsisTrust.GuidelineUI
{
    public class ReflectiveGuidelineUITemplateSelector : IGuidelineUITemplateSelector
    {
        private readonly Dictionary<Type, Type> _blockTemplatesDictionary;

        public ReflectiveGuidelineUITemplateSelector()
        {
            // Generate the block templates dictionary.
            _blockTemplatesDictionary = new Dictionary<Type, Type>
                                        {
                                            {typeof(ActionBlock), typeof(ActionBlockTemplate)},
                                            {typeof(AssessmentBlock), typeof(AssessmentBlockTemplate)},
                                            {typeof(SummaryBlock), typeof(SummaryBlockTemplate)}
                                        };
        }

        public View SelectUIForBlock(Block block)
        {
            var blockType = block.GetType();

            if (_blockTemplatesDictionary.ContainsKey(blockType))
            {
                var templateType = _blockTemplatesDictionary[blockType];
                return Activator.CreateInstance(templateType) as View;
            }

            return new Label {Text = "Failed to find block."};
        }
    }
}