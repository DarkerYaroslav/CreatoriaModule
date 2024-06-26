using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatoriaModule.Config
{
    public class datacfg
    {
        public bool? GoldPatch;
        public bool? GrenadePatch;
        public bool? MarkerPatch;
        public bool? NicknamePatch;
        public bool? VoicePatch;
        public datacfg(bool? gpatch,bool? grpatch,bool? mpatch,bool? npatch,bool? vpatch) 
        {
            GoldPatch = gpatch;
            GrenadePatch = grpatch;
            MarkerPatch = mpatch;
            NicknamePatch = npatch;
            VoicePatch = vpatch;
        }
    }
}
