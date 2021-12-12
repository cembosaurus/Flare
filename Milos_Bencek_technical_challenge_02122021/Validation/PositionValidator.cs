using FluentValidation;
using FluentValidation.Results;
using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Interfaces;
using Milos_Bencek_technical_challenge_02122021.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Milos_Bencek_technical_challenge_02122021.Validation
{
    class PositionValidator : AbstractValidator<Position>
    {
        private static readonly PositionValidator _positionValidator = new PositionValidator();

        static PositionValidator() { }
        private PositionValidator() { Setup(); }

        public static PositionValidator Position => _positionValidator;


        private void Setup()
        {
            RuleFor(p => p.startX)
                .GreaterThan(0)
                .WithMessage("Start position X should be larger than 0!");
            RuleFor(p => p.startY)
                .GreaterThan(0)
                .WithMessage("Start position Y should be larger than 0!");
            RuleFor(p => p.endX)
                .GreaterThan(0)
                .WithMessage("End position X should be larger than 0!");
            RuleFor(p => p.endY)
                .GreaterThan(0)
                .WithMessage("End position Y should be larger than 0!");

            RuleFor(p => p.endX)
                .GreaterThanOrEqualTo(p => p.startX)
                .WithMessage("End position X should be equal or higher than start position X!");
            RuleFor(p => p.endY)
                .GreaterThanOrEqualTo(p => p.startY)
                .WithMessage("End position Y should be equal or higher than start position Y!");

            RuleFor(p => p.Board.Grid.Length)
                .GreaterThan(p => p.Board.SizeX)
                .WithMessage("Array size must be larger than horizontal line!");

            RuleFor(p => p.Board.Grid.Length % p.Board.SizeX)
                .Equal(0)
                .WithMessage("Board must be rectangle. Array size must be integral multiple of size of horizontal line!");

            RuleFor(p => p.endY)
                .Equal(p => p.startY)
                .When(p => p.endX > p.startX)
                .WithMessage("Incorrect end position Y value. Direction of placed ship should be horizontal or vertical!");
            RuleFor(p => p.endX)
                .Equal(p => p.startX)
                .When(p => p.endY > p.startY)
                .WithMessage("Incorrect end position X value. Direction of placed ship should be horizontal or vertical!");

            RuleFor(p => p.endX)
                .LessThanOrEqualTo(p => p.Board.SizeX)
                .WithMessage("Ship must not overlap horizontal border of board!");
            RuleFor(p => p.endY)
                .LessThanOrEqualTo(p => p.Board.Grid.Length / p.Board.SizeX)
                .WithMessage("Ship must not overlap vertical border of board!");
                    
        }
    }
}
