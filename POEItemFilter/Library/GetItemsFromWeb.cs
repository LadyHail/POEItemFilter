using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using POEItemFilter.Library.Enumerables;
using POEItemFilter.Models;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.Library
{
    public class GetItemsFromWeb
    {
        private string RawWebData { get; set; }
        private RegexOptions Options { get; set; }

        private ApplicationDbContext _context;

        public GetItemsFromWeb()
        {
            _context = new ApplicationDbContext();
            Options = RegexOptions.Singleline;
            GetNewItems();
        }

        private void GetNewItems()
        {
            Hashtable downloadingLinks = new Hashtable();

            foreach (BaseTypes itemBaseType in Enum.GetValues(typeof(BaseTypes)))
            {
                downloadingLinks = GetWebsitesLinks(itemBaseType);

                foreach (DictionaryEntry link in downloadingLinks)
                {
                    SaveNewBaseType(itemBaseType.ToString());
                    SaveNewType(itemBaseType.ToString(), link.Key.ToString());

                    RawWebData = GetWebsiteText(link.Value.ToString());

                    Regex[] queries = SelectQuery(itemBaseType, link.Key.ToString());

                    if (itemBaseType == BaseTypes.Armour)
                    {
                        GetArmoursItems((Types)link.Key, queries[0], queries[1]);
                    }
                    else
                    {
                        MatchCollection items = RegexRequest(queries[0], RawWebData);

                        foreach (Match item in items)
                        {
                            SaveNewItem(item, (Types)link.Key, itemBaseType);
                        }
                    }
                    _context.SaveChanges();
                }
            }
        }

        private Regex[] SelectQuery(BaseTypes itemBaseType, string itemType)
        {
            Regex[] output = new Regex[2];
            Regex itemsQuery = null;
            Regex attributesQuery = null;

            switch (itemBaseType)
            {
                case BaseTypes.SkillGem:
                    itemsQuery = new Regex("/><a href=\"/.+?\" title=\"(?<name>.+?)\".+?>Requires Level.+?value\">(?<level>[0-9]+)", Options, new TimeSpan(0, 0, 5));
                    break;

                case BaseTypes.LabyrinthItem:
                    itemsQuery = new Regex("_activator\"><a href=\"/.+?\" title=\"(?<name>.+?)\".+?>", Options, new TimeSpan(0, 0, 1));
                    break;

                case BaseTypes.Piece:
                    itemsQuery = new Regex("/><a href=\"/.+?\" title=\"(?<name>.+?)\".+?>", Options, new TimeSpan(0, 0, 1));
                    break;

                case BaseTypes.DivinationCard:
                    itemsQuery = new Regex("></span><span class=\"divicard-header\">(?<name>.+?)</span>", Options, new TimeSpan(0, 0, 5));
                    break;

                case BaseTypes.Armour:
                    if (itemType == Types.BodyArmour.ToString())
                    {
                        attributesQuery = new Regex($"Body Armours \\(<a href=\".+?\" title=\"(?<attribute1>.+?)\">.+? title=\"(?<attribute2>.+?)\".+?</table>", Options, new TimeSpan(0, 0, 1));
                    }
                    else
                    {
                        attributesQuery = new Regex($"{itemType}s \\(<a href=\".+?\" title=\"(?<attribute1>.+?)\">.+? title=\"(?<attribute2>.+?)\".+?</table>", Options, new TimeSpan(0, 0, 1));
                    }

                    itemsQuery = new Regex("_activator\"><a href=\"/.+?\" title=\"(?<name>.+?)\".+?>Requires Level.+?value\">(?<level>[0-9]+)", Options, new TimeSpan(0, 0, 1));
                    break;

                default:
                    itemsQuery = new Regex("_activator\"><a href=\"/.+?\" title=\"(?<name>.+?)\".+?>.+?<td data-sort-value=\"(?<level>[0-9]+)\"", Options, new TimeSpan(0, 0, 1));
                    break;
            }
            output[0] = itemsQuery;
            output[1] = attributesQuery;

            return output;
        }

        private void SaveNewItem(Match item, Types itemType, BaseTypes itemBaseType)
        {
            ItemDB newItem = new ItemDB();
            newItem.Name = item.Groups["name"].Value;

            if (item.Groups["level"].Value != "")
            {
                newItem.Level = byte.Parse(item.Groups["level"].Value);
            }

            newItem.BaseType = _context.BaseTypes.SingleOrDefault(i => i.Name == itemBaseType.ToString());

            newItem.Type = _context.Types.SingleOrDefault(i => i.Name == itemType.ToString());

            bool isItemInDb = _context.ItemsDB.Select(i => i.Name).Contains(newItem.Name);

            if (!isItemInDb)
            {
                _context.ItemsDB.Add(newItem);
            }
        }

        private void GetArmoursItems(Types itemType, Regex itemsQuery, Regex attributesQuery)
        {
            MatchCollection attributes = RegexRequest(attributesQuery, RawWebData);

            foreach (Match attributeMatch in attributes)
            {
                MatchCollection items = RegexRequest(itemsQuery, attributeMatch.ToString());

                foreach (Match item in items)
                {
                    string attribute1Value = attributeMatch.Groups["attribute1"].Value;
                    string attribute2Value = attributeMatch.Groups["attribute2"].Value;

                    if (attribute2Value != "Strength" & attribute2Value != "Dexterity" & attribute2Value != "Intelligence")
                    {
                        attribute2Value = null;
                    }

                    SaveArmourItem(item, itemType, attribute1Value, attribute2Value);
                }
                _context.SaveChanges();
            }
        }

        private void SaveArmourItem(Match item, Types itemType, string attribute1Value, string attribute2Value)
        {
            SaveNewAttribute(attribute1Value);
            ConnectAttributeAndType(attribute1Value, itemType.ToString());
            ConnectAttributeAndBaseType(attribute1Value);

            SaveNewAttribute(attribute2Value);
            ConnectAttributeAndType(attribute2Value, itemType.ToString());
            ConnectAttributeAndBaseType(attribute2Value);

            ItemDB newItem = new ItemDB();
            newItem.Name = item.Groups["name"].Value;

            if (item.Groups["level"].Value != "")
            {
                newItem.Level = byte.Parse(item.Groups["level"].Value);
            }

            newItem.BaseType = _context.BaseTypes.SingleOrDefault(i => i.Name == BaseTypes.Armour.ToString());

            newItem.Type = _context.Types.SingleOrDefault(i => i.Name == itemType.ToString());

            var attribute1 = _context.Attributes.SingleOrDefault(i => i.Name == attribute1Value);
            newItem.Attributes.Add(attribute1);

            if (attribute2Value != null)
            {
                var attribute2 = _context.Attributes.SingleOrDefault(i => i.Name == attribute2Value);
                newItem.Attributes.Add(attribute2);
            }

            bool isItemInDb = _context.ItemsDB.Select(i => i.Name).Contains(newItem.Name);
            if (!isItemInDb)
            {
                _context.ItemsDB.Add(newItem);
            }
        }

        private bool WebsiteAvailability(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                request.AllowAutoRedirect = false;
                request.Method = "HEAD";

                using (WebResponse response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private string GetWebsiteText(string url)
        {
            string rawWebData = "";
            string cacheFileName = ComputeHash(url);
            string cachePath = @".\App_Data\Cache\" + cacheFileName + ".cache";
            if (File.Exists(cachePath) && (DateTime.Now - File.GetLastWriteTime(cachePath)).TotalMinutes < 1440)
            {
                rawWebData = File.ReadAllText(cachePath);
                return rawWebData;
            }
            else
            {
                if (!WebsiteAvailability(url))
                {
                    return rawWebData;
                }

                WebClient webClient = new WebClient();
                rawWebData = webClient.DownloadString(url);
                if (!Directory.Exists(Path.GetDirectoryName(cachePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(cachePath));
                }
                File.WriteAllText(cachePath, rawWebData);
                return rawWebData;
            }
        }

        private string ComputeHash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private MatchCollection RegexRequest(Regex regex, string rawWebData)
        {
            Match match = null;
            MatchCollection output = null;

            try
            {
                match = regex.Match(rawWebData);
            }
            catch (TimeoutException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }

            output = regex.Matches(rawWebData);
            return output;
        }

        private void SaveNewBaseType(string itemBaseType)
        {
            bool isBaseTypeInDb = _context.BaseTypes.Select(i => i.Name).Contains(itemBaseType);
            if (!isBaseTypeInDb)
            {
                ItemBaseType newBaseType = new ItemBaseType();
                newBaseType.Name = itemBaseType.ToString();
                _context.BaseTypes.Add(newBaseType);
                _context.SaveChanges();
            }
        }

        private void SaveNewType(string itemBaseType, string itemType)
        {
            bool isTypeInDb = _context.Types.Select(i => i.Name).Contains(itemType);
            if (!isTypeInDb)
            {
                ItemType newType = new ItemType();
                newType.Name = itemType;
                var existingBaseType = _context.BaseTypes.SingleOrDefault(i => i.Name == itemBaseType.ToString());
                newType.BaseTypeId = existingBaseType.Id;
                _context.Types.Add(newType);
                _context.SaveChanges();
            }
        }

        private void SaveNewAttribute(string attribute)
        {
            if (attribute != null)
            {
                bool isAttribueInDb = _context.Attributes.Select(i => i.Name).Contains(attribute);
                if (!isAttribueInDb)
                {
                    ItemAttribute newAttribute = new ItemAttribute();
                    newAttribute.Name = attribute;
                    _context.Attributes.Add(newAttribute);
                    _context.SaveChanges();
                }
            }
        }

        private void ConnectAttributeAndType(string attribute, string itemType)
        {
            if (attribute != null)
            {
                var attributeInDb = _context.Attributes.SingleOrDefault(a => a.Name == attribute);
                var typeInDb = _context.Types.SingleOrDefault(t => t.Name == itemType);

                typeInDb.Attributes.Add(attributeInDb);
                _context.SaveChanges();
            }
        }

        private void ConnectAttributeAndBaseType(string attribute)
        {
            if (attribute != null)
            {
                var attributeInDb = _context.Attributes.SingleOrDefault(a => a.Name == attribute);
                var baseTypeInDb = _context.BaseTypes.SingleOrDefault(b => b.Name == BaseTypes.Armour.ToString());

                attributeInDb.BaseTypeId = baseTypeInDb.Id;
                _context.SaveChanges();
            }
        }

        private Hashtable GetWebsitesLinks(BaseTypes itemBaseType)
        {
            Hashtable links = new Hashtable();

            switch (itemBaseType)
            {
                case BaseTypes.Armour: // armours_list
                    links.Add(Types.BodyArmour, "https://pathofexile.gamepedia.com/List_of_body_armours");
                    links.Add(Types.Boot, "https://pathofexile.gamepedia.com/List_of_boots");
                    links.Add(Types.Glove, "https://pathofexile.gamepedia.com/List_of_gloves");
                    links.Add(Types.Helmet, "https://pathofexile.gamepedia.com/List_of_helmets");
                    links.Add(Types.Shield, "https://pathofexile.gamepedia.com/List_of_shields");
                    break;

                case BaseTypes.Weapon: // weapons_list
                    links.Add(Types.Bow, "https://pathofexile.gamepedia.com/List_of_bows");
                    links.Add(Types.Claw, "https://pathofexile.gamepedia.com/List_of_claws");
                    links.Add(Types.Dagger, "https://pathofexile.gamepedia.com/List_of_daggers");
                    links.Add(Types.OneHandAxe, "https://pathofexile.gamepedia.com/List_of_one_hand_axes");
                    links.Add(Types.OneHandMace, "https://pathofexile.gamepedia.com/List_of_one_hand_maces");
                    links.Add(Types.OneHandSword, "https://pathofexile.gamepedia.com/List_of_one_hand_swords");
                    links.Add(Types.Sceptre, "https://pathofexile.gamepedia.com/List_of_sceptres");
                    links.Add(Types.Stave, "https://pathofexile.gamepedia.com/List_of_staves");
                    links.Add(Types.TwoHandAxe, "https://pathofexile.gamepedia.com/List_of_two_hand_axes");
                    links.Add(Types.TwoHandMace, "https://pathofexile.gamepedia.com/List_of_two_hand_maces");
                    links.Add(Types.TwoHandSword, "https://pathofexile.gamepedia.com/List_of_two_hand_swords");
                    links.Add(Types.Wand, "https://pathofexile.gamepedia.com/List_of_wands");
                    links.Add(Types.ThrustingOneHandSword, "https://pathofexile.gamepedia.com/List_of_thrusting_one_hand_swords");
                    break;

                case BaseTypes.Accessory: // accessories_list
                    links.Add(Types.Amulet, "https://pathofexile.gamepedia.com/List_of_amulets");
                    links.Add(Types.Belt, "https://pathofexile.gamepedia.com/List_of_belts");
                    links.Add(Types.Ring, "https://pathofexile.gamepedia.com/List_of_rings");
                    links.Add(Types.Quiver, "https://pathofexile.gamepedia.com/List_of_quivers");
                    break;

                case BaseTypes.Currency: // currencies_list
                    links.Add(Types.Currency, "https://pathofexile.gamepedia.com/Currency#Discontinued_currency_items");
                    break;

                case BaseTypes.SkillGem: // skill_gems_list
                    links.Add(Types.SupportSkillGem, "https://pathofexile.gamepedia.com/List_of_support_skill_gems");
                    links.Add(Types.ActiveSkillGem, "https://pathofexile.gamepedia.com/Active_skill_gem");
                    break;

                case BaseTypes.Map: // maps_list
                    links.Add(Types.Map, "https://pathofexile.gamepedia.com/List_of_base_maps");
                    break;

                case BaseTypes.Flask: // flasks_list
                    links.Add(Types.UtilityFlask, "https://pathofexile.gamepedia.com/List_of_all_utility_flasks");
                    links.Add(Types.LifeFlask, "https://pathofexile.gamepedia.com/List_of_life_flasks");
                    links.Add(Types.ManaFlask, "https://pathofexile.gamepedia.com/List_of_mana_flasks");
                    links.Add(Types.HybridFlask, "https://pathofexile.gamepedia.com/List_of_hybrid_flasks");
                    break;

                case BaseTypes.Essence: // essences_list
                    links.Add(Types.Essence, "https://pathofexile.gamepedia.com/List_of_essences");
                    break;

                case BaseTypes.LabyrinthItem: // labyrinth_items_list
                    links.Add(Types.LabyrinthTrinket, "https://pathofexile.gamepedia.com/List_of_labyrinth_trinkets");
                    links.Add(Types.LabyrinthItem, "https://pathofexile.gamepedia.com/Labyrinth_Item");
                    break;

                case BaseTypes.FishingRod: // fishing_rods_list
                    links.Add(Types.FishingRod, "https://pathofexile.gamepedia.com/Fishing_rod");
                    break;

                case BaseTypes.Piece: // pieces_list
                    links.Add(Types.Piece, "https://pathofexile.gamepedia.com/Piece");
                    break;

                case BaseTypes.DivinationCard: // divination_cards_list
                    links.Add(Types.DivinationCard, "https://pathofexile.gamepedia.com/Divination_card");
                    break;
            }
            return links;
        }
    }
}