using System;
using Guidelines.Model;
using Guidelines.Model.DataBag;
using Prism.Events;
using SepsisTrust.Messages.Guideline;
using static System.String;

namespace Guidelines.Extensions
{
    /// <summary>
    ///     Provides base functionality to allow classes to extend Guideline features.
    /// </summary>
    public class GuidelineExtension
    {
        protected IEventAggregator EventAggregator;

        public GuidelineExtension( IEventAggregator eventAggregator )
        {
            EventAggregator = eventAggregator;
        }
    }

    public class AuditGuidelineExtension : GuidelineExtension
    {
        public AuditGuidelineExtension( IEventAggregator eventAggregator ) : base(eventAggregator)
        {
            eventAggregator.GetEvent<GuidelineEntityFinishedEvent>().Subscribe(EntityFinished);
        }

        private void EntityFinished( IGuidelineEntity guidelineEntity )
        {
            var entity = guidelineEntity as IDataBagEntity;
            if ( entity?.DataBag != null && ( entity?.DataBag ).ContainsKey("InsightsId") )
            {
                var insightsId = entity?.DataBag["InsightsId"];
                if ( !IsNullOrEmpty(insightsId) )
                {
                    var block = guidelineEntity as Block;
                    var blockTitle = block?.Title;
                    var id = insightsId;
                }
            }
        }
    }
}