namespace CreatoriaModule.Config
{
    public class JsonConfiguration(
        bool? goldPatch,
        bool? grenadePatch, 
        bool? markerPatch, 
        bool? nicknamePatch,
        bool? voicePatch)
    {
        public bool? GoldPatch = goldPatch;
        public bool? GrenadePatch = grenadePatch;
        public bool? MarkerPatch = markerPatch;
        public bool? NicknamePatch = nicknamePatch;
        public bool? VoicePatch = voicePatch;
    }
}
