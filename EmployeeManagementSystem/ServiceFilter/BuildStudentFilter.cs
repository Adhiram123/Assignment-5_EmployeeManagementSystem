using EmployeeManagementSystem.Entites;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.ServiceFilter
{
    public class BuildStudentFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is StudentFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("object is null");

            }
            StudentFilterCriteria filterCriteriaStudent = (StudentFilterCriteria)param.Value;
            var statusFilter = filterCriteriaStudent.Filters.Find(a => a.FieldName == "status");
            if ((statusFilter == null))
            {
                statusFilter = new FilterCriteria();
                statusFilter.FieldName = "status";
                statusFilter.FieldValue = "Active";
                filterCriteriaStudent.Filters.Add(statusFilter);
            }
            filterCriteriaStudent.Filters.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));
            var result = await next();
        }
    }
}
