using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shold_Community.Entities.Resources
{
    class RequestSResources
    {
        
        public RequestRes[] Property1 { get; set; }
    }
    public class RequestRes
    {
        
        public int id { get; set; }
        public int worldId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public int villageId { get; set; }
        public int amount { get; set; }
        public int onWay { get; set; }
        public int max_quantum { get; set; }
        public int playerId { get; set; }
        public long timestamp { get; set; }
        public long currentTimestamp { get; set; }

        public void translateFromBD(string currentUICulture)
        {
            switch (currentUICulture)
            {
                case "en":
                    switch (this.name)
                    {
                        case "00":
                            this.name = "wood";
                            break;
                        case "01":
                            this.name = "stone";
                            break;
                        case "02":
                            this.name = "iron";
                            break;
                        case "03":
                            this.name = "pitch";
                            break;


                        case "10":
                            this.name = "banq1";
                            break;
                        case "11":
                            this.name = "banq2";
                            break;
                        case "12":
                            this.name = "banq3";
                            break;
                        case "13":
                            this.name = "banq4";
                            break;
                        case "14":
                            this.name = "banq5";
                            break;
                        case "15":
                            this.name = "banq6";
                            break;


                        case "20":
                            this.name = "apple";
                            break;
                        case "21":
                            this.name = "cheese";
                            break;
                        case "22":
                            this.name = "meat";
                            break;
                        case "23":
                            this.name = "bread";
                            break;
                        case "24":
                            this.name = "vegetables";
                            break;
                        case "25":
                            this.name = "fish";
                            break;


                        case "30":
                            this.name = "ale";
                            break;


                        case "40":
                            this.name = "bows";
                            break;
                        case "41":
                            this.name = "armor+pickes";
                            break;
                        case "42":
                            this.name = "armor+swords";
                            break;
                        case "43":
                            this.name = "pickes";
                            break;
                        case "44":
                            this.name = "armor";
                            break;
                        case "45":
                            this.name = "swords";
                            break;
                        case "46":
                            this.name = "catapults";
                            break;
                        default:
                            this.name = "empty";
                            break;

                    }
                    break;

                case "ru":
                    switch (this.name)
                    {
                        case "00":
                            this.name = "дерево";
                            break;
                        case "01":
                            this.name = "камень";
                            break;
                        case "02":
                            this.name = "железо";
                            break;
                        case "03":
                            this.name = "смола";
                            break;


                        case "10":
                            this.name = "оленина";
                            break;
                        case "11":
                            this.name = "стулья";
                            break;
                        case "12":
                            this.name = "посуда";
                            break;
                        case "13":
                            this.name = "одежда";
                            break;
                        case "14":
                            this.name = "вино";
                            break;
                        case "15":
                            this.name = "соль";
                            break;
                        case "16":
                            this.name = "специи";
                            break;
                        case "17":
                            this.name = "шелк";
                            break;


                        case "20":
                            this.name = "яблоки";
                            break;
                        case "21":
                            this.name = "сыр";
                            break;
                        case "22":
                            this.name = "мясо";
                            break;
                        case "23":
                            this.name = "хлеб";
                            break;
                        case "24":
                            this.name = "овощи";
                            break;
                        case "25":
                            this.name = "рыба";
                            break;


                        case "30":
                            this.name = "эль";
                            break;


                        case "40":
                            this.name = "луки";
                            break;
                        case "41":
                            this.name = "бронепики";
                            break;
                        case "42":
                            this.name = "бронемечи";
                            break;
                        case "43":
                            this.name = "пики";
                            break;
                        case "44":
                            this.name = "броня";
                            break;
                        case "45":
                            this.name = "мечи";
                            break;
                        case "46":
                            this.name = "катапульты";
                            break;
                        default:
                            this.name = "пусто";
                            break;

                    }
                    break;

            }
            
            //return requestRes;
        }
    }

}
