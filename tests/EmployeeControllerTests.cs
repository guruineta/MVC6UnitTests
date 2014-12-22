using Xunit;
using Controllers;
using Models;
using Moq;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System;

namespace Tests
{
	public class EmployeeControllerTests
	{
private EmployeeController _employeeController;
private Mock<IEmployeeRepository> _employeeRepository;
public EmployeeControllerTests()
{
	_employeeRepository = new Mock<IEmployeeRepository>();
	_employeeController = new EmployeeController(_employeeRepository.Object);
	var mockModelMetadataProvider = new Mock<IModelMetadataProvider>();
	var viewDataDictionary = new ViewDataDictionary<object>(mockModelMetadataProvider.Object);
	_employeeController.ViewData = viewDataDictionary;
}

[Fact]
public void VerifyIndexDisplaysAllEmployees()
{
	_employeeRepository.Setup(x => x.FindAll()).Returns(new List<Employee>()
	{
		new Employee() { Id = 1, Name = "Employee 1" },
		new Employee() { Id = 2, Name = "Employee 2" },
		new Employee() { Id = 3, Name = "Employee 3" }
	});

	var indexResult = _employeeController.Index() as ViewResult;

	Assert.NotNull(indexResult);
	var employees = indexResult.ViewData.Model as List<Employee>;
	Assert.Equal(3, employees.Count);
	Assert.Equal(1, employees[0].Id);
	Assert.Equal("Employee 3", employees[2].Name);
}

[Fact]
public void VerifyDetailsReturns404IfEmployeeIdIsNull()
{
	_employeeRepository
		.Setup(x => x.Get(It.IsAny<int?>())).Returns<Employee>(null);

	var httpStatusCodeResult 
		= _employeeController.Details(null) as HttpStatusCodeResult;

	Assert.NotNull(httpStatusCodeResult);
	Assert.Equal(404, httpStatusCodeResult.StatusCode);
}

[Fact]
public void VerifyDetailsReturns404IfEmployeeNotFound()
{
	_employeeRepository
		.Setup(x => x.Get(It.IsAny<int?>())).Returns<Employee>(null);

	var httpStatusCodeResult 
		= _employeeController.Details(1) as HttpStatusCodeResult;

	Assert.NotNull(httpStatusCodeResult);
	Assert.Equal(404, httpStatusCodeResult.StatusCode);
}

[Fact]
public void VerifyDetailsReturnsEmployee()
{
	_employeeRepository.Setup(x => x.Get(It.IsAny<int?>())).
		Returns((int id) => new Employee() { Id = id, Name = "Employee " + id });

	var viewResult = _employeeController.Details(1) as ViewResult;

	Assert.NotNull(viewResult);
	var employee = viewResult.ViewData.Model as Employee;
	Assert.NotNull(employee);
	Assert.Equal(1, employee.Id);
}

		[Fact]
		public void VerifyCreateEmployeeRedirectsToError()
		{
			_employeeRepository.Setup(x => x.Save(It.IsAny<Employee>()));
			var employee = new Employee() { Id = 1 };
			//Mocking the _employeeController.ModelState.IsValid = false
			_employeeController.ModelState.AddModelError("Error", "Name is Required");

			var createResult = _employeeController.Create(employee) as ViewResult;

			Assert.NotNull(createResult);
			Assert.Equal("Create", createResult.ViewName);
		}

		[Fact]
		public void VerifyCreateEmployeeInsertData()
		{
			_employeeRepository.Setup(x => x.Save(It.IsAny<Employee>())).Verifiable();
			var employee = new Employee() { 
				Id = 1, 
				Name = "Employee", 
				Designation = "Designation", 
				JoiningDate = DateTime.Now };

			var createResult = _employeeController.Create(employee) as RedirectToActionResult;
			Assert.NotNull(createResult);
			Assert.Equal("Index", createResult.ActionName);
			_employeeRepository.Verify();
		}
	}
}