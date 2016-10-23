using System;
using Guidelines.Model;
using SepsisTrust.GuidelineUI.Views;
using Xamarin.Forms;

namespace SepsisTrust.GuidelineUI
{
    public class ReflectiveGuidelineUITemplateSelector : IGuidelineUITemplateSelector
    {
        public View SelectUIForBlock( Block block )
        {
            if ( block is SummaryBlock )
            {
                return new SummaryBlockTemplate();
            }

            if ( block is ActionBlock )
            {
                return new ActionBlockTemplate();
            }

            if ( block is AssessmentBlock )
            {
                var assessmentBlock = (AssessmentBlock) block;

                switch ( assessmentBlock.AssessmentType )
                {
                    case AssessmentType.Question:
                        return new QuestionAssessmentBlockTemplate();
                    case AssessmentType.Checklist:
                        return new AssessmentBlockTemplate();
                    default:
                        return new AssessmentBlockTemplate();
                }
            }

            return new Label {Text = "Failed to find block."};
        }
    }
}