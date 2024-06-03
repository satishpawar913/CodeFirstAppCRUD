namespace CodeFirstApproachCRUD.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string message, Exception ex) : base(message)
        { 

        }
        public EmployeeNotFoundException(string? message) : base(message)
        {
                
        }
    }

    public class DepartmentNotFoundException : Exception
    {
        public DepartmentNotFoundException(string? message) : base(message)
        {
        }

        public DepartmentNotFoundException(string message, Exception ex) : base(message)
        {

        }
    }
}
