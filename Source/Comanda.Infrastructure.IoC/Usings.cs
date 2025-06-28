/* global usings for the System namespaces here */

global using System.Diagnostics.CodeAnalysis;

/* global usings for the Microsoft namespaces here */

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Hosting;

/* global usings for the Comanda namespaces here */

global using Comanda.Shared.Configuration;
global using Comanda.Domain.Entities;
global using Comanda.Domain.Interfaces.Repositories;

global using Comanda.Application.Gateways;
global using Comanda.Application.Handlers;
global using Comanda.Application.Services;
global using Comanda.Application.Payloads;
global using Comanda.Application.Validators;
global using Comanda.Application.Interfaces;

global using Comanda.Infrastructure.Repositories;
global using Comanda.Infrastructure.Gateways;
global using Comanda.Infrastructure.Monitoring;
global using Comanda.Infrastructure.Providers;
global using Comanda.Infrastructure.IoC.Helpers;

/* global usings for third-party namespaces here */

global using MongoDB.Bson;
global using MongoDB.Bson.Serialization;
global using MongoDB.Bson.Serialization.Serializers;
global using MongoDB.Driver;

global using FluentValidation;
global using FluentValidation.AspNetCore;


