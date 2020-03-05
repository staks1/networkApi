using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetworkApi.Models;
using NetworkApi.Models.Dtos;
using NetworkApi.Repository.IRepository;
using System.Collections.Generic;

namespace NetworkApi.Controllers
{
    //api controller !
    [Route("api/[controller]")]
    [ApiController]
    public class NationalNetworkController : ControllerBase
    {

        private readonly INationalNetworkRepository _nationalNetworkRepository;
        private readonly IMapper _mapper;

        public NationalNetworkController(INationalNetworkRepository nationalNetworkRepository, IMapper mapper)
        {
            _nationalNetworkRepository = nationalNetworkRepository;
            _mapper = mapper;
        }

        //define http get method to get all NationalNetworks
        public IActionResult GetNationalNetworks()
        {

            var networks = _nationalNetworkRepository.GetNationalNetworks();
            //do not expose the model 
            // only expose the dto 

            var networksDto = new List<NationalNetworkDto>();

            foreach (var net in networks)
            {
                networksDto.Add(_mapper.Map<NationalNetworkDto>(net));
            }

            return Ok(networksDto);
        }



        //define http get method for getting a certain Network based on Region Name
        //set up the parameter type else it fails-collides with the previous get
        [HttpGet("{nationalNetworkId:int}", Name = "GetNationalNetwork")]
        public IActionResult GetNationalNetwork(int nationalNetworkId)
        {
            var network = _nationalNetworkRepository.GetNationalNetwork(nationalNetworkId);
            if (network == null)
            {
                return NotFound();
            }
            var networkDto = _mapper.Map<NationalNetworkDto>(network);
            return Ok(networkDto);
        }

        /* maybe  define get to return the networks based on searched region->network name (string)*/

        //define http post method to create new nationalNetwork 
        [HttpPost]
        [Authorize(Roles = "provider")]
        public IActionResult CreateNationalNetwork([FromBody] NationalNetworkDto nationalNetworkDto)
        {
            //null Dto
            if (nationalNetworkDto == null)
            {
                return BadRequest(ModelState);
            }
            //if it does not exist 
            if (_nationalNetworkRepository.NationalNetworkExists(nationalNetworkDto.Name))
            {
                ModelState.AddModelError("", "This Network already exists!");
                return StatusCode(404, ModelState);
            }
            //if state not valid 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // all correct! create the new network ,return it using the mapper!-->create the model

            var newNationalNetwork = _mapper.Map<NationalNetwork>(nationalNetworkDto);
            //check if record was created successfully or not 

            if (!_nationalNetworkRepository.CreateNationalNetwork(newNationalNetwork))
            {
                ModelState.AddModelError("", $"Something is wrong with the creation of the record{newNationalNetwork.Name}");
                return StatusCode(500, ModelState);
            }

            //return the created object instead of a simple ok answer 
            return CreatedAtRoute("GetNationalNetwork", new { nationalNetworkId = newNationalNetwork.Id }, newNationalNetwork);

        }

        //define patch method to update the listings
        [Authorize(Roles = "provider")]
        [HttpPatch("{nationalNetworkId:int}", Name = "UpdateNationalNetwork")]
        public IActionResult UpdateNationalNetwork(int nationalNetworkId, [FromBody] NationalNetworkDto nationalNetworkDto)
        {
            //null Dto
            if (nationalNetworkDto == null || nationalNetworkId != nationalNetworkDto.Id)
            {
                return BadRequest(ModelState);
            }


            //create the network 
            var newNationalNetwork = _mapper.Map<NationalNetwork>(nationalNetworkDto);
            //check if record was created successfully or not 

            if (!_nationalNetworkRepository.UpdateNationalNetwork(newNationalNetwork))
            {
                ModelState.AddModelError("", $"Something is wrong with the upating of the record{newNationalNetwork.Name}");
                return StatusCode(500, ModelState);
            }



            return NoContent();

        }

        //define the delete method to remove records 

        [HttpDelete("{nationalNetworkId:int}", Name = "DeleteNationalNetwork")]
        [Authorize(Roles = "provider")]
        public IActionResult DeleteNationalNetwork(int nationalNetworkId)
        {
            //null Dto not exists 
            if (!_nationalNetworkRepository.NationalNetworkExists(nationalNetworkId))
            {
                return NotFound();
            }


            //retrieve and delete the network 
            var newNationalNetwork = _nationalNetworkRepository.GetNationalNetwork(nationalNetworkId);
            //check if record was delete successfully or not 

            if (!_nationalNetworkRepository.DeleteNationalNetwork(newNationalNetwork))
            {
                ModelState.AddModelError("", $"Something is wrong with the deletion of the record{newNationalNetwork.Name}");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, "success at deletion");

        }


    }
}