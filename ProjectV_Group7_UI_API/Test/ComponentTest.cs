// using Bunit;
// using Xunit;
// using ProjectV_Group7_UI_API.Pages;  // Adjust based on your actual namespaces
// using ProjectV_Group7_UI_API.Data;    // If you're using services, adjust accordingly
// using Microsoft.Extensions.DependencyInjection;
// using FluentAssertions;

// public class ComponentTests : TestContext
// {
//     // Test the rendering of CriticalNotificationHistory component
//     [Fact]
//     public void CriticalNotificationHistoryRendersCorrectly()
//     {
//         // Arrange
//         var component = RenderComponent<CriticalNotification>();

//         // Act & Assert
//         component.Markup.Contains("Critical Notification History").Should().BeTrue();
//         component.Markup.Contains("New Notification").Should().BeTrue();
//         component.Markup.Contains("View a history of all notifications.").Should().BeTrue();

//         // Check if table columns are rendered
//         component.Markup.Contains("Date").Should().BeTrue();
//         component.Markup.Contains("Recipient").Should().BeTrue();
//         component.Markup.Contains("Type").Should().BeTrue();
//         component.Markup.Contains("Status").Should().BeTrue();
//         component.Markup.Contains("Priority").Should().BeTrue();
//     }

//     // Test that clicking the "New Notification" button shows the modal
//     [Fact]
//     public void ClickingNewNotificationButtonShowsModal()
//     {
//         // Arrange
//         var component = RenderComponent<CriticalNotification>();

//         // Find the "New Notification" button and click it
//         var newNotificationButton = component.Find("button.btn-new-notification");
//         newNotificationButton.Click();

//         // Assert: Ensure that the AddNotificationModal shows up
//         var modal = component.FindComponent<AddNotificationModal>();
//         modal.Instance.isVisible.Should().BeTrue();
//     }

//     // Test the rendering and interactions of AddNotificationModal
//     [Fact]
//     public void AddNotificationModalRendersAndSavesNotification()
//     {
//         // Arrange
//         var component = RenderComponent<AddNotificationModal>();

//         // Find and interact with input elements
//         var notesField = component.Find("textarea#notes");
//         var recipientDropdown = component.Find("select#recipient");
//         var typeField = component.Find("input#type");
//         var statusDropdown = component.Find("select#status");
//         var priorityDropdown = component.Find("select#priority");
//         var saveButton = component.Find("button.btn-primary");

//         notesField.Change("Test Notes");
//         recipientDropdown.Change("Recipient 1");
//         typeField.Change("Test Type");
//         statusDropdown.Change("Pending");
//         priorityDropdown.Change("High");

//         // Act: Click the Save button to simulate adding the notification
//         saveButton.Click();

//         // Assert: Check if the notification service was called or data was saved (mock or check service state)
//         // For testing purposes, you could assert that some service method is called here
//     }

//     // Test modal visibility toggle
//     [Fact]
//     public void ModalVisibilityWorksCorrectly()
//     {
//         // Arrange
//         var component = RenderComponent<AddNotificationModal>();

//         // Assert: Modal should initially be hidden
//         component.Instance.isVisible.Should().BeFalse();

//         // Act: Show the modal
//         component.Instance.Show();
//         component.Instance.isVisible.Should().BeTrue();

//         // Act: Hide the modal
//         component.Instance.Hide();
//         component.Instance.isVisible.Should().BeFalse();
//     }

//     // Test that the AddNotificationModal is closed correctly after saving
//     [Fact]
//     public void AddNotificationModalClosesAfterSaving()
//     {
//         // Arrange
//         var component = RenderComponent<AddNotificationModal>();

//         // Set input values
//         component.Find("textarea#notes").Change("Test Notes");
//         component.Find("select#recipient").Change("Recipient 1");

//         // Act: Save the notification
//         var saveButton = component.Find("button.btn-primary");
//         saveButton.Click();

//         // Assert: Check if the modal is closed after saving
//         component.Instance.isVisible.Should().BeFalse();
//     }
// }
