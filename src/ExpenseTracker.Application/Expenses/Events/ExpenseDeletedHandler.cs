﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Raven.Client.Documents.Session;

namespace ExpenseTracker.Application.Expenses.Events
{
    /// <summary>
    /// Represents the handler for the <see cref="ExpenseDeleted"/> event.
    /// </summary>
    public sealed class ExpenseDeletedHandler : INotificationHandler<ExpenseDeleted>
    {
        private readonly IAsyncDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDeletedHandler"/> class.
        /// </summary>
        /// <param name="session">The async document session instance.</param>
        public ExpenseDeletedHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        public Task Handle(ExpenseDeleted notification, CancellationToken cancellationToken)
        {
            if (notification.ExpenseId == Guid.Empty)
            {
                return Task.CompletedTask;
            }

            _session.Delete(notification.ExpenseId.ToString());

            return Task.CompletedTask;
        }
    }
}