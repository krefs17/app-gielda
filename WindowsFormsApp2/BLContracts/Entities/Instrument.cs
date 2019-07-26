using System.Collections.Generic;

namespace BLContracts.Entities
{ 
    public class Instrument
    {
        public string Nazwa { get; set; }
        public List<Notowanie> Notowania { get; set; }
    }
}
