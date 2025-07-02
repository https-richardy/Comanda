/* global usings for the System namespaces here */

global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using System.Net;
global using System.Net.Http.Headers;

/* global usings for the Microsoft namespaces here */

global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Http;

/* global usings for the Comanda namespaces here */

global using Comanda.Domain.Entities;
global using Comanda.Domain.Filters;
global using Comanda.Domain.Interfaces.Repositories;

global using Comanda.Application.Gateways;
global using Comanda.Application.Payloads;
global using Comanda.Application.Interfaces;

global using Comanda.Shared.Results;
global using Comanda.Shared.Errors;
global using Comanda.Shared.Configuration;

global using Comanda.Infrastructure.Constants;
global using Comanda.Infrastructure.Stages;
global using Comanda.Infrastructure.Serializers;


/* global usings for third-party namespaces here */

global using MongoDB.Driver;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization;

global using Stripe.Checkout;