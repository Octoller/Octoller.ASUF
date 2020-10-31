/*
 * **************************************************************************************************************************
 *     _    ____  _   _ _____ 
 *    / \  / ___|| | | |  ___|
 *   / _ \ \___ \| | | | |_   
 *  / ___ \ ___) | |_| |  _|  
 * /_/   \_\____/ \___/|_|  
 * 
 * Octoller.ASUF
 * Library
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {

    /// <summary>
    /// Filter class used in the process of processing file download notifications
    /// </summary>
    public class TempFilter {

        /// <summary>
        /// Root folder for sorting files of the selected type.
        /// </summary>
        public virtual string RootFolderPatch {
            get; set;
        }

        /// <summary>
        /// Path of the last subfolder.
        /// </summary>
        public virtual string LastFolderPatch {
            get; set;
        }


        /// <summary>
        /// The size above which the new subfolder will be created.
        /// </summary>
        public virtual double Limit {
            get; set;
        }


        /// <summary>
        /// Current limit counter.
        /// </summary>
        public virtual double Counter {
            get; set;
        }


        /// <summary>
        /// Сhecks if the size is exceeded.
        /// </summary>
        /// <returns>Returns true if the size is exceeded.</returns>
        public virtual bool IsExcess() =>
            Counter > Limit;


        /// <summary>
        /// Reason for creating a new subfolder.
        /// </summary>
        public virtual ReasonCreatingFolder ReasonCreating {
            get; set;
        }

        /// <summary>
        /// Construction.
        /// </summary>
        /// <param name="root">Root folder for sorting files of the selected type.</param>
        /// <param name="limit">The size above which the new subfolder will be created.</param>
        public TempFilter(string root, double limit) {

            if (limit <= 0) {
                Limit = 200;
            } else {
                Limit = limit;
            }

            RootFolderPatch = root;
            
        }
    }
}
