using System.Collections.Generic;
using System.Text.Json;
using JiraApi.Helpers;
using JiraApi.Objects.Jira;
using NUnit.Framework;

namespace JiraApi.Tests;

[TestFixture]
public class JiraHelpersTests
{
    private Dictionary<string, JsonElement> _fieldNames = [];

    [SetUp]
    public void SetUp()
    {
        _fieldNames = new Dictionary<string, JsonElement>
        {
            { "field1", JsonDocument.Parse("123.45").RootElement },
            { "field2", JsonDocument.Parse("\"test\"").RootElement }
        };
    }

    [Test]
    public void ParamsToPath_AllParams_ReturnsCorrectPath()
    {
        // Arrange
        var projectParams = new ProjectParams
        {
            StartAt = 10,
            MaxResults = 50,
            OrderBy = "name",
            Id = [1, 2],
            Keys = ["KEY1", "KEY2"],
            Query = "test",
            TypeKey = "type",
            CategoryId = 123,
            Action = "do",
            Expand = "fields"
        };

        // Act
        var result = JiraHelpers.ParamsToPath(projectParams);

        // Assert
        Assert.That(result,
            Is.EqualTo(
                "?&startAt=10&maxResults=50&orderBy=name&id=1&id=2&keys=KEY1&keys=KEY2&query=test&typeKey=type&categoryId=123&action=do&expand=fields"));
    }

    [Test]
    public void ParamsToPath_SomeParams_ReturnsCorrectPath()
    {
        // Arrange
        var projectParams = new ProjectParams
        {
            StartAt = 10,
            MaxResults = 50,
            Keys = ["KEY1", "KEY2"],
            Query = "test",
            Expand = "fields"
        };

        // Act
        var result = JiraHelpers.ParamsToPath(projectParams);

        // Assert
        Assert.That(result, Is.EqualTo("?&startAt=10&maxResults=50&keys=KEY1&keys=KEY2&query=test&expand=fields"));
    }

    [Test]
    public void ParamsToPath_NoParams_ReturnsQuestionMark()
    {
        // Arrange
        var projectParams = new ProjectParams();

        // Act
        var result = JiraHelpers.ParamsToPath(projectParams);

        // Assert
        Assert.That(result, Is.EqualTo("?"));
    }

    [Test]
    public void ToStringValue_ExistingField_ReturnsValue()
    {
        var result = _fieldNames.ToStringValue("field2");

        Assert.That(result, Is.EqualTo("\"test\""));
    }

    [Test]
    public void ToStringValue_NonExistingField_ReturnsNull()
    {
        var result = _fieldNames.ToStringValue("field3");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ToStringValue_NullFieldNames_ReturnsNull()
    {
        _fieldNames = null;

        var result = _fieldNames.ToStringValue("field1");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ToStringValue_NullJsonValue_ReturnsNull()
    {
        var result = _fieldNames.ToStringValue("field3");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ToFloatValue_ExistingField_ReturnsValue()
    {
        var result = _fieldNames.ToFloatValue("field1");

        Assert.That(result, Is.EqualTo(123.45f));
    }

    [Test]
    public void ToFloatValue_NonExistingField_ReturnsMinusOne()
    {
        var result = _fieldNames.ToFloatValue("field3");

        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void ToFloatValue_NullFieldNames_ReturnsMinusOne()
    {
        _fieldNames = null;

        var result = _fieldNames.ToFloatValue("field1");

        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void ToFloatValue_NotANumber_ReturnsMinusOne()
    {
        var result = _fieldNames.ToFloatValue("field2");

        Assert.That(result, Is.EqualTo(null));
    }
}
