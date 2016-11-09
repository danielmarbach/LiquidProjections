using System;

namespace LiquidProjections
{
    /// <summary>
    /// Tracks the progress of projectors and provides statistics about their status for diagnostic purposes.
    /// </summary>
    public class StatisticsService
    {
        private Func<DateTime> p;
        private readonly IStoreProjectionStatistics projectionStatisticsStore;

        public StatisticsService(IStoreProjectionStatistics projectionStatisticsStore)
        {
            if (projectionStatisticsStore == null)
            {
                throw new ArgumentNullException(nameof(projectionStatisticsStore));
            }

            this.projectionStatisticsStore = projectionStatisticsStore;
        }

        public StatisticsService(IStoreProjectionStatistics projectionStatisticsStore, Func<DateTime> p) : this(projectionStatisticsStore)
        {
            this.p = p;
        }

        /// <summary>
        /// Gets an object that can be used to track the progress of a particular projector.
        /// </summary>
        public ProjectionStatistics GetForProjector(string projectorId)
        {
            return new ProjectionStatistics(projectorId, projectionStatisticsStore);
        }

        public TimeSpan GetTimeToCatchup(string projectorId, long targetCheckpoint)
        {
            var stats = GetForProjector(projectorId);
            return stats.GetTimeToCatchup(targetCheckpoint);
        }
    }

    public class ProjectionStatistics
    {
        private readonly string projectorId;
        private readonly IStoreProjectionStatistics projectionStatisticsStore;

        internal ProjectionStatistics(string projectorId, IStoreProjectionStatistics projectionStatisticsStore)
        {
            this.projectorId = projectorId;
            this.projectionStatisticsStore = projectionStatisticsStore;
        }

        public void TrackCheckpoint(long lastProcessedCheckpoint)
        {
            
        }

        public TimeSpan GetTimeToCatchup(long targetCheckpoint)
        {
            return TimeSpan.Zero;
        }
    }
}