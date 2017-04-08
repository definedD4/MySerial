using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySerial.Model;
using MySerial.Repository.LocalRepository;
using Newtonsoft.Json.Linq;

namespace LocalRepositoryTests
{
    [TestClass]
    public class SerialDescriptorTest
    {
        [TestMethod]
        public void Parse()
        {
            string text = @"
            {
                'title': 'Test title',
                'description': 'Test description',
                'playlist': [
                    {
                        'title': 'Season 1',
                        'episodes': [
                            {
                                'title': 'Episode 1.1',
                                'media': 'C:/Test/Path/media_1_1.mp4'
                            },
                            {
                                'title': 'Episode 1.2',
                                'media': 'C:/Test/Path/media_1_2.mp4'
                            }
                        ]
                    }
                ]
            }
            ";

            SerialDescriptor expected = new SerialDescriptor("Test title", "Test description", new SerialPlaylist(new[]
            {
                new Season("Season 1", new[]
                {
                    new Episode("Episode 1.1", new LocalMediaSource(@"C:/Test/Path/media_1_1.mp4")),
                    new Episode("Episode 1.2", new LocalMediaSource(@"C:/Test/Path/media_1_2.mp4")),
                }),
            }));

            SerialDescriptor actual = SerialDescriptor.Parse(text);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToJson()
        {
            var descriptor = new SerialDescriptor("Test title", "Test description", new SerialPlaylist(new[]
            {
                new Season("Season 1", new[]
                {
                    new Episode("Episode 1.1", new LocalMediaSource(@"C:/Test/Path/media_1_1.mp4")),
                    new Episode("Episode 1.2", new LocalMediaSource(@"C:/Test/Path/media_1_2.mp4")),
                }),
            }));

            var expected = JObject.FromObject(new
            {
                title = "Test title",
                description = "Test description",
                playlist = new []
                {
                    new
                    {
                        title = "Season 1",
                        episodes = new []
                        {
                            new
                            {
                                title = "Episode 1.1",
                                media = @"C:/Test/Path/media_1_1.mp4"
                            },
                            new
                            {
                                title = "Episode 1.2",
                                media = @"C:/Test/Path/media_1_2.mp4"
                            }
                        }
                    }
                }           
            });

            var actual = JObject.Parse(descriptor.ToJson());

            Assert.IsTrue(JToken.DeepEquals(expected, actual));
        }
    }
}
