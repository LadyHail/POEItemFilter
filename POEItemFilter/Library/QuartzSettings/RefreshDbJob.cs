using Quartz;

namespace POEItemFilter.Library.QuartzSettings
{
    public class RefreshDbJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            GetItemsFromWeb refreshDb = new GetItemsFromWeb();
        }
    }
}