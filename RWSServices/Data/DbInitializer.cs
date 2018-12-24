using Microsoft.AspNetCore.Identity;
using RWSServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RWSServices.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                          RoleManager<ApplicationRole> roleManager)
        {
            //Re-create database if model changes
            context.Database.EnsureCreated();

            if (context.Blogs.Any())
            {
                return;   // DB has been seeded
            }

            var user = await userManager.FindByNameAsync("admin@rwsservices.net");
            
            var blogs = new Blog[]
            {
                new Blog{BlogTitle="First Blog",BlogImage="1.jpg",BlogContent="Blog content...",CreatedDate=DateTime.Parse("2018-04-01"),CreatedBy=user.Id},
                new Blog{BlogTitle="Second Blog",BlogImage="2.jpg",BlogContent="Blog content...",CreatedDate=DateTime.Parse("2018-04-02"),CreatedBy=user.Id},
                new Blog{BlogTitle="Third Blog",BlogImage="3.png",BlogContent="Blog content...",CreatedDate=DateTime.Parse("2018-04-02"),CreatedBy=user.Id},
                new Blog{BlogTitle="Fourth Blog",BlogImage="4.jpg",BlogContent="Blog content...",CreatedDate=DateTime.Parse("2018-04-02"),CreatedBy=user.Id},
                new Blog{BlogTitle="Fifth Blog",BlogImage="5.jpg",BlogContent="Blog content...",CreatedDate=DateTime.Parse("2018-04-02"),CreatedBy=user.Id},
                new Blog{BlogTitle="Sixth Blog",BlogImage="6.jpg",BlogContent="Blog content...",CreatedDate=DateTime.Parse("2018-04-02"),CreatedBy=user.Id},
                new Blog{BlogTitle="Seventh Blog",BlogImage="7.jpg",BlogContent="Blog content...",CreatedDate=DateTime.Parse("2018-04-02"),CreatedBy=user.Id}
            };
            foreach (Blog b in blogs)
            {
                await context.Blogs.AddAsync(b);
            }
            await context.SaveChangesAsync();

            var emojiCategories = new EmojiCategory[]
            {
                new EmojiCategory{Category="Smileys & People"}
            };
            foreach (EmojiCategory c in emojiCategories)
            {
                await context.EmojiCategories.AddAsync(c);
            }
            context.SaveChanges();

            var emojis = new Emoji[]
            {
                new Emoji{EmojiCode="😀",Description="grinning face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😁",Description="beaming face with smiling eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😂",Description="face with tears of joy",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤣",Description="rolling on the floor laughing",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😃",Description="grinning face with big eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😄",Description="grinning face with smiling eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😅",Description="grinning face with sweat",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😆",Description="grinning squinting face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😉",Description="winking face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😊",Description="smiling face with smiling eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😋",Description="face savoring food",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😎",Description="smiling face with sunglasses",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😍",Description="smiling face with heart-eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😘",Description="face blowing a kiss",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🥰",Description="smiling face with 3 hearts",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😗",Description="kissing face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😙",Description="kissing face with smiling eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😚",Description="kissing face with closed eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="☺",Description="smiling face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙂",Description="slightly smiling face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤗",Description="hugging face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤩",Description="star-struck",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤔",Description="thinking face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤨",Description="face with raised eyebrow",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😐",Description="neutral face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😑",Description="expressionless face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😶",Description="face without mouth",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙄",Description="face with rolling eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😏",Description="smirking face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😣",Description="persevering face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😥",Description="sad but relieved face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😮",Description="face with open mouth",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤐",Description="zipper-mouth face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😯",Description="hushed face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😪",Description="sleepy face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😫",Description="tired face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😴",Description="sleeping face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😌",Description="relieved face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😛",Description="face with tongue",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😜",Description="winking face with tongue",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😝",Description="squinting face with tongue",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤤",Description="drooling face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😒",Description="unamused face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😓",Description="downcast face with sweat",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😔",Description="pensive face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😕",Description="confused face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙃",Description="upside-down face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤑",Description="money-mouth face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😲",Description="astonished face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="☹",Description="frowning face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙁",Description="slightly frowning face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😖",Description="confounded face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😞",Description="disappointed face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😟",Description="worried face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😤",Description="face with steam from nose",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😢",Description="crying face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😭",Description="loudly crying face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😦",Description="frowning face with open mouth",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😧",Description="anguished face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😨",Description="fearful face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😩",Description="weary face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤯",Description="exploding head",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😬",Description="grimacing face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😰",Description="anxious face with sweat",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😱",Description="face screaming in fear",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🥵",Description="hot face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🥶",Description="cold face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😳",Description="flushed face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤪",Description="zany face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😵",Description="dizzy face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😡",Description="pouting face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😠",Description="angry face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤬",Description="face with symbols on mouth",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😷",Description="face with medical mask",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤒",Description="face with thermometer",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤕",Description="face with head-bandage",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤢",Description="nauseated face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤮",Description="face vomiting",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤧",Description="sneezing face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😇",Description="smiling face with halo",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤠",Description="cowboy hat face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🥳",Description="partying face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🥴",Description="woozy face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🥺",Description="pleading face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤥",Description="lying face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤫",Description="shushing face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤭",Description="face with hand over mouth",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🧐",Description="face with monocle",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤓",Description="nerd face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😈	",Description="smiling face with horns",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👿",Description="angry face with horns",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤡",Description="clown face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👹",Description="ogre",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👺",Description="goblin",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="💀",Description="skull",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="☠",Description="skull and crossbones",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👻",Description="ghost",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👽",Description="alien",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👾",Description="alien monster",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🤖",Description="robot face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="💩",Description="pile of poo",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😺",Description="grinning cat face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😸",Description="grinning cat face with smiling eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😹",Description="cat face with tears of joy",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😻",Description="smiling cat face with heart-eyes",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😼",Description="cat face with wry smile",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😽",Description="kissing cat face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙀",Description="weary cat face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😿",Description="crying cat face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="😾",Description="pouting cat face",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙈",Description="see-no-evil monkey",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙉",Description="hear-no-evil monkey",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🙊",Description="speak-no-evil monkey",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👶",Description="baby",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🧒",Description="child",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👦",Description="boy",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👧",Description="girl",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🧑",Description="adult",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨",Description="man",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩",Description="woman",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="🧓",Description="older adult",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👴",Description="old man",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👵",Description="old woman",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍⚕️",Description="man health worker",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍⚕️",Description="woman health worker",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🎓",Description="man student",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🎓",Description="woman student",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🏫",Description="man teacher",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🏫",Description="woman teacher",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍⚖️",Description="man judge",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍⚖️",Description="woman judge",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🌾",Description="man farmer",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🌾",Description="woman farmer",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🍳",Description="man cook",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🍳",Description="woman cook",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🔧",Description="man mechanic",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🔧",Description="woman mechanic",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🏭",Description="man factory worker",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🏭",Description="woman factory worker",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍💼",Description="man office worker",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍💼",Description="woman office worker",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🔬",Description="man scientist",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🔬",Description="woman scientist",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍💻",Description="man technologist",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍💻",Description="woman technologist",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🎤",Description="man singer",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🎤",Description="woman singer",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍🎨",Description="man artist",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍🎨",Description="woman artist",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👨‍✈️",Description="man pilot",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID},
                new Emoji{EmojiCode="👩‍✈️",Description="woman pilot",EmojiCategoryID=emojiCategories.Single( e => e.Category == "Smileys & People").EmojiCategoryID}
            };
            foreach (Emoji e in emojis)
            {
                await context.Emojis.AddAsync(e);
            }
            await context.SaveChangesAsync();
        }
    }
}
