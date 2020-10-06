namespace Octoller.ASUF.Kernel.ServiceObjects {
    public interface ITempFilter {
        SortFilter CurrentFilter {
            get; set;
        }

        string LastFolderPatch {
            get; set;
        }

        double Counter {
            get; set;
        }

        bool isExcess {
            get;
        }
    }
}
