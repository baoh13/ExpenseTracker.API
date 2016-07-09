using ExpenseTracker.Repository;
using ExpenseTracker.Repository.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseTracker.API.Controllers
{
    public class ExpenseGroupsController : ApiController
    {
        private IExpenseTrackerRepository _repository;
        private ExpenseGroupFactory _expenseGroupFactory = new ExpenseGroupFactory();
        
        public ExpenseGroupsController()
        {
            _repository = new ExpenseTrackerEFRepository(new Repository.Entities.ExpenseTrackerContext());
        }

        public ExpenseGroupsController(ExpenseTrackerEFRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var expenseGroups = _repository.GetExpenseGroups()
                                               .ToList()
                                               .Select(eg => _expenseGroupFactory.CreateExpenseGroup(eg));

                return Ok(expenseGroups);
            }

            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
