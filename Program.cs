using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LiteDB;


namespace fallguyslevelchances
{
    public class Program
    {

        private static Dictionary<String, String> roundNames = new Dictionary<String, String> {
                    { "round_door_dash" , "Door Dash" },
                    { "round_gauntlet_02" , "Dizzy Heights" },
                    { "round_dodge_fall" , "Fruit Chute" },
                    { "round_chompchomp" , "Gate Crash" },
                    { "round_gauntlet_01" , "Hit Parade" },
                    { "round_see_saw" , "See Saw" },
                    { "round_lava" , "Slime Climb" },
                    { "round_tip_toe" , "Tip Toe" },
                    { "round_gauntlet_03" , "Whirlygig" },
                    { "round_gauntlet_04", "Knight Fever" },
                    { "round_wall_guys", "Wall Guys" },
                    { "round_hoops_blockade_solo" , "Hoopsie Legends"},
                    { "round_block_party" , "Block Party"},
                    { "round_jump_club" , "Jump Club"},
                    { "round_match_fall" , "Perfect Match"},
                    { "round_tunnel" , "Roll Out" },
                    { "round_tail_tag" , "Tail Tag" },
                    { "round_egg_grab" , "Egg Scramble" },
                    { "round_fall_ball_60_players" , "Fall Ball"} ,
                    { "round_ballhogs" , "Hoarders" } ,
                    { "round_hoops" , "Hoopsie Daisy" } ,
                    { "round_jinxed" , "Jinxed" } ,
                    { "round_rocknroll" , "Rock'n'Roll" } ,
                    { "round_conveyor_arena" , "Team Tail Tag" } ,
                    { "round_egg_grab_02" , "Egg Siege" } ,
                    { "round_fall_mountain_hub_complete" , "RNG Mountain" } ,
                    { "round_floor_fall" , "Hex-a-Gone" } ,
                    { "round_jump_showdown" , "Jump Showdown" } ,
                    { "round_royal_rumble" , "Royal Fumble" }
            };




        public class RoundInfo {
            public LiteDB.ObjectId Id { get; set; }
            public string Name { get; set; }
            public int Players { get; set; }
            public int ShowID {get; set; }
            public int Round {get; set; }
            public Boolean InParty {get; set; }

            public int Position {get; set;}
            public int Tier { get; set; }
            public override string ToString()
            {
                return string.Format("Id : {0}, Name : {1}, Players : {2}",
                    Id,
                    Name,
                    Players);
            }
    }

        static void Main(string[] args)
        {
            using(var db = new LiteDatabase(@"data.db"))
            {
                Console.WriteLine("Please insert a numerical value 1-60.");
                var numinput = 0;
                bool numFound = false;
                while (numFound == false) {
                    try {
                        numinput = int.Parse(Console.ReadLine());
                    } catch(FormatException) {
                        Console.Write("Please insert a numerical value.");
                    }
                    if (numinput < 1 || numinput > 60) {
                        Console.Write("Value must be between 1-60.");
                    } else {
                        numFound = true;
                    }
                }
                
                

                // Get customer collection

                var RoundDetails = db.GetCollection<RoundInfo>("RoundDetails");
                bool hasOneresult = false;

                foreach(KeyValuePair<string, string> entry in roundNames) {
                    var query = RoundDetails
                    .Find(x => x.Players == numinput && x.Name.Contains(entry.Key));
                    if (query.Count() != 0) {
                        Console.Write(entry.Value + ": ");
                        Console.WriteLine(query.Count());
                        hasOneresult = true;
                    }
                }

                if (hasOneresult == false) {
                        Console.WriteLine("Sorry, no results were found for that number of players.");
                    }


            }
         
            
        }
    }
}