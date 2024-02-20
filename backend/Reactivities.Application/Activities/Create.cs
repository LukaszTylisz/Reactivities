﻿using MediatR;
using Reactivities.Domain;
using Reactivities.Persistence;

namespace Reactivities.Application.Activities;

public class Create
{
    public class Command : IRequest
    {
        public Activity Activity { get; set; }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _dataContext;
        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            _dataContext.Activities.Add(request.Activity);

            await _dataContext.SaveChangesAsync();
        }
    }
}