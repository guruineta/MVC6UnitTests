using System.Collections.Generic;

namespace Models
{
	public interface IEmployeeRepository
	{
		Employee Get(int? id);
		void Save(Employee employee);
		void Delete(Employee employee);
		void Update(Employee employee);
		IEnumerable<Employee> FindAll();
	}
}