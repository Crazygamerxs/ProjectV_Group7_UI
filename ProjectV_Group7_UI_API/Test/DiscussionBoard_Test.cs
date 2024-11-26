// using Bunit;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using YourProject.Pages; // Update with the actual namespace of your component

// namespace YourProject.Tests
// {
//     [TestClass]
//     public class DiscussionBoardTests : TestContext
//     {
//         [TestMethod]
//         public async Task OnInitializedAsync_ShouldLoadDisastersAndChats()
//         {
//             // Arrange
//             var component = RenderComponent<DiscussionBoard>();

//             // Act
//             await component.InvokeAsync(() => component.Instance.OnInitializedAsync());

//             // Assert
//             var disasters = component.Instance.Disasters;
//             var chats = component.Instance.Chats;

//             Assert.IsNotNull(disasters);
//             Assert.IsNotNull(chats);
//             Assert.AreEqual(disasters.Count, 0); // Assuming no disasters are initially loaded
//             Assert.AreEqual(chats.Count, 0); // Assuming no chats are initially loaded
//         }

//         [TestMethod]
//         public void OnSearchChanged_ShouldFilterDisastersBasedOnSearchQuery()
//         {
//             // Arrange
//             var component = RenderComponent<DiscussionBoard>();
//             component.Instance.Disasters = new List<DiscussionBoard.Disaster>
//             {
//                 new DiscussionBoard.Disaster { id = 1, name = "Earthquake" },
//                 new DiscussionBoard.Disaster { id = 2, name = "Flood" },
//                 new DiscussionBoard.Disaster { id = 3, name = "Hurricane" }
//             };

//             // Act
//             component.Instance.SearchQuery = "Earthquake";

//             // Assert
//             var filteredDisasters = component.Instance.FilteredDisasters;
//             Assert.AreEqual(1, filteredDisasters.Count);
//             Assert.AreEqual("Earthquake", filteredDisasters.First().name);
//         }

//         [TestMethod]
//         public void ShowAddNewPopup_ShouldDisplayPopupWhenCalled()
//         {
//             // Arrange
//             var component = RenderComponent<DiscussionBoard>();

//             // Act
//             component.Instance.ShowAddNewPopup();

//             // Assert
//             var popup = component.Find(".popup-backdrop");
//             Assert.IsNotNull(popup);
//         }

//         [TestMethod]
//         public async Task AddNewDisaster_ShouldAddDisasterAndSave()
//         {
//             // Arrange
//             var component = RenderComponent<DiscussionBoard>();
//             component.Instance.Disasters = new List<DiscussionBoard.Disaster>();

//             // Act
//             component.Instance.NewDisasterName = "Tornado";
//             await component.InvokeAsync(() => component.Instance.AddNewDisaster());

//             // Assert
//             var disasters = component.Instance.Disasters;
//             Assert.AreEqual(1, disasters.Count);
//             Assert.AreEqual("Tornado", disasters.First().name);
//         }

//         [TestMethod]
//         public async Task SendMessage_ShouldAddNewChatAndSave()
//         {
//             // Arrange
//             var component = RenderComponent<DiscussionBoard>();
//             component.Instance.SelectedDisasterId = 1;
//             component.Instance.SelectedChats = new List<DiscussionBoard.Chat>();

//             // Act
//             component.Instance.NewMessage = "This is a test message";
//             await component.InvokeAsync(() => component.Instance.SendMessage());

//             // Assert
//             var chats = component.Instance.SelectedChats;
//             Assert.AreEqual(1, chats.Count);
//             Assert.AreEqual("This is a test message", chats.First().message);
//         }

//         [TestMethod]
//         public void SelectDisaster_ShouldSelectCorrectDisasterAndLoadChats()
//         {
//             // Arrange
//             var component = RenderComponent<DiscussionBoard>();
//             component.Instance.Disasters = new List<DiscussionBoard.Disaster>
//             {
//                 new DiscussionBoard.Disaster { id = 1, name = "Earthquake" },
//                 new DiscussionBoard.Disaster { id = 2, name = "Flood" }
//             };

//             // Act
//             component.Instance.SelectDisaster(1);

//             // Assert
//             Assert.AreEqual(1, component.Instance.SelectedDisasterId);
//             Assert.AreEqual("Earthquake", component.Instance.SelectedDisaster.name);
//             Assert.IsNotNull(component.Instance.SelectedChats);
//         }

//         [TestMethod]
//         public void OnSearchChanged_ShouldClearSearchWhenEmpty()
//         {
//             // Arrange
//             var component = RenderComponent<DiscussionBoard>();
//             component.Instance.Disasters = new List<DiscussionBoard.Disaster>
//             {
//                 new DiscussionBoard.Disaster { id = 1, name = "Earthquake" },
//                 new DiscussionBoard.Disaster { id = 2, name = "Flood" },
//                 new DiscussionBoard.Disaster { id = 3, name = "Hurricane" }
//             };

//             // Act
//             component.Instance.SearchQuery = ""; // Clear the search query

//             // Assert
//             var filteredDisasters = component.Instance.FilteredDisasters;
//             Assert.AreEqual(3, filteredDisasters.Count); // All disasters should be shown
//         }
//     }
// }
