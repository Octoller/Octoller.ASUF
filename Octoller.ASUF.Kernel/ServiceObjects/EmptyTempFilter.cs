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
 * 11.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {

    public class EmptyTempFilter : TempFilter {

        public override string LastFolderPatch {
            get; set;
        }

        public override bool IsExcess() => false;

        public EmptyTempFilter() : base(string.Empty, 0) { }
    }
}
