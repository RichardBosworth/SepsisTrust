using System;
using Guidelines.Model;

namespace SepsisTrust.Model
{
    public static class TemporaryGuidelineGenerator
    {
        public static Guideline Generate()
        {
/*
            // Generate guideline.
            var guideline = new Guideline
            {
                Title = "ED/AMU Sepsis Screening & Action Tool",
                Description = "To be applied to all non-pregnant adults and young people over 12 years with fever\r\n(or recent fever) symptoms, or who are clearly unwell with any abnormal observations",
                Identifier = "ED-ADULT"
            };

            // Generate first phase.
            var assessmentPhase = new Phase(guideline)
            {
                Identifier = "assessment"
            };

            var lowRiskBlock = new SummaryBlock(assessmentPhase)
            {
                Title = "Low Risk of Sepsis",
                SummaryText = "Use standard protocols, consider discharge (approved\r\nby senior decision maker) with safety netting",
                Identifier = "lowrisk"
            };

            var highRiskBlock = new SummaryBlock(assessmentPhase)
            {
                Title = "High Risk of Sepsis",
                SummaryText = "Use Sepsis Six",
                Identifier = "highrisk",
                SummaryImagePath = ""
            };

            var infectionBlock = new AssessmentBlock(assessmentPhase)
            {
                Title = "Could this be due to an infection?",
                Identifier = "infection-area",
                BlockActivities = {new BlockActivityData("Yes, but source unclear at present"), new BlockActivityData("Pneumonia"), new BlockActivityData("Urinary Tract Infection"), new BlockActivityData("Abdominal pain or distension"), new BlockActivityData("Cellulitis/ septic arthritis/ infected wound")},
                Links = {new Link(1, Double.MaxValue, highRiskBlock)}
            };

            var sickBlock = new AssessmentBlock(assessmentPhase)
            {
                Title = "Does the patient look sick?",
                Identifier = "sick",
                BlockActivities = {new BlockActivityData("Does the patient look sick?"), new BlockActivityData("NEWS >3")},
                Links = {new Link(0, 0.9, lowRiskBlock), new Link(1, Double.MaxValue, infectionBlock)}
            };

            assessmentPhase.Blocks.Add(lowRiskBlock);
            assessmentPhase.Blocks.Add(highRiskBlock);
            assessmentPhase.Blocks.Add(infectionBlock);
            assessmentPhase.Blocks.Add(sickBlock);
            assessmentPhase.EntryBlock = sickBlock;

            // Register links.
            guideline.LinkManager.Register(lowRiskBlock);
            guideline.LinkManager.Register(highRiskBlock);
            guideline.LinkManager.Register(infectionBlock);
            guideline.LinkManager.Register(sickBlock);
            guideline.LinkManager.Register(assessmentPhase);

            guideline.Phases.Add(assessmentPhase);
            guideline.EntryPhase = assessmentPhase;

            return guideline;

            
*/

            return new Guideline();
        }
    }
}