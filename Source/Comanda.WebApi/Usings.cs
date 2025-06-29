global using System.Diagnostics.CodeAnalysis;
global using System.Security.Claims;
global using System.Text.Json;

/* global usings for the Microsoft namespaces here */

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;

/* global usings for the Comanda namespaces here */

global using Comanda.WebApi.Extensions;
global using Comanda.Infrastructure.Constants;
global using Comanda.Infrastructure.IoC.Extensions;
global using Comanda.Application.Payloads;

/* global usings for third-party namespaces here */

global using HealthChecks.UI.Client;
global using MediatR;