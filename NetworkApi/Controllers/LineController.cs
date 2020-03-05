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
    public class LineController : ControllerBase
    {

        private readonly ILineRepository _LineRepository;
        private readonly IMapper _mapper;

        public LineController(ILineRepository LineRepository, IMapper mapper)
        {
            _LineRepository = LineRepository;
            _mapper = mapper;
        }

        //define http get method to get all Lines
        public IActionResult GetLines()
        {

            var Lines = _LineRepository.GetLines();
            //do not expose the model 
            // only expose the dto 

            var LinesDto = new List<LineDto>();

            foreach (var line in Lines)
            {
                LinesDto.Add(_mapper.Map<LineDto>(line));
            }

            return Ok(LinesDto);
        }



        //define http get method for getting a certain Network based on Region Name
        //set up the parameter type else it fails-collides with the previous get
        [HttpGet("{LineId:int}", Name = "GetLine")]
        public IActionResult GetLine(int LineId)
        {
            var Line = _LineRepository.GetLine(LineId);
            if (Line == null)
            {
                return NotFound();
            }
            var LinesDto = _mapper.Map<LineDto>(Line);
            return Ok(LinesDto);
        }

        /* maybe  define get to return the networks based on searched region->network name (string)*/

        //define http post method to create new Line 
        [HttpPost]
        [Authorize(Roles = "admin, provider")]
        public IActionResult CreateLine([FromBody] LineDto LineDto)
        {
            //null Dto
            if (LineDto == null)
            {
                return BadRequest(ModelState);
            }

            //if state not valid 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // all correct! create the new network ,return it using the mapper!-->create the model

            var newLine = _mapper.Map<Line>(LineDto);
            //check if record was created successfully or not 

            if (!_LineRepository.CreateLine(newLine))
            {
                ModelState.AddModelError("", $"Something is wrong with the creation of the record{newLine.Id}");
                return StatusCode(500, ModelState);
            }

            //return the created object instead of a simple ok answer 
            return CreatedAtRoute("GetLine", new { LineId = newLine.Id }, newLine);

        }

        //define patch method to update the listings
        [Authorize(Roles = "admin, provider")]
        [HttpPatch("{LineId:int}", Name = "UpdateLine")]
        public IActionResult UpdateLine(int LineId, [FromBody] LineDto LineDto)
        {
            //null Dto
            if (LineDto == null || LineId != LineDto.Id)
            {
                return BadRequest(ModelState);
            }


            //create the network 
            var newLine = _mapper.Map<Line>(LineDto);
            //check if record was created successfully or not 

            if (!_LineRepository.UpdateLine(newLine))
            {
                ModelState.AddModelError("", $"Something is wrong with the upating of the record{newLine.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        //define the delete method to remove records 

        [HttpDelete("{LineId:int}", Name = "DeleteLine")]
        [Authorize(Roles = "admin, provider")]
        public IActionResult DeleteLine(int LineId)
        {
            //null Dto not exists 
            if (!_LineRepository.LineExists(LineId))
            {
                return NotFound();
            }


            //retrieve and delete the network 
            var newLine = _LineRepository.GetLine(LineId);
            //check if record was delete successfully or not 

            if (!_LineRepository.DeleteLine(newLine))
            {
                ModelState.AddModelError("", $"Something is wrong with the deletion of the record{newLine.Id}");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, "success at deletion");

        }


    }
}