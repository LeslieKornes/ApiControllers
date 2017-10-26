using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiControllers.Controllers
{
    // The route by which API controllers are reached can be defined only using the Route attribute and cannot be defined in the application configuration in the Startup class. The convention
    // for API controllers is to use a route prefixed with 'api', followed by the name of the controller, so that the ReservationController controller is reached through the URL /api/reservation.
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository repository;

        // API controllers are instantiated in the same way as regular controllers, which means thaty they can declare dependencies that will be resolved using the service provider. The ReservationController 
        // class declares a ctor dependency on the IRepository interface, which will be resolved to provide access to the data in the model.
        public ReservationController(IRepository repo)
        {
            repository = repo;
        }

        /// Each action method is decorated iwht an attribute that specifies the HTTP method it accepts. Routes can bbe further refined my including a routing fragment as the argument to the HTTP method...

        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;


        //In this case, it means that this action can be reached by sending a GET request whose URL matches the /api/reservations/{id} routing pattern, where the id segment is then used to identify the reservation
        //object that should be retrieved
        [HttpGet("{id}")]
        public Reservation Get(int id) => repository[id];

        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            repository.AddReservation(new Reservation
            {
                ClientName = res.ClientName,
                Location = res.Location
            });

        [HttpPut]
        public Reservation Put([FromBody] Reservation res) =>
            repository.UpdateReservation(res);

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
