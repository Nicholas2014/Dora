﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dora.ExceptionHandling.Test
{
    public class ExceptionPolicyBuilderFixture
    {
        [Fact]
        public void New_Arguments_Not_Allow_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new ExceptionPolicyBuilder(null));
        }

        [Fact]
        public void New_Normal()
        {
            var serviceProvider = new ServiceCollection().BuildServiceProvider();
            Assert.Same(serviceProvider, new ExceptionPolicyBuilder(serviceProvider).ServiceProvider);
        }

        [Theory]
        [InlineData(null, "1")]
        [InlineData("1", null)]
        public void For_Arguments_Not_Allow_Null(string typeIndicator, string configureIndicator)
        {
            Type exceptionType = typeIndicator == null
                ? null
                : typeof(Exception);
            Action<IExceptionHandlerBuilder> configure = configureIndicator == null
                ? null as Action<IExceptionHandlerBuilder>
                : _ => { };
            var builder = new ExceptionPolicyBuilder(new ServiceCollection().BuildServiceProvider());
            Assert.Throws<ArgumentNullException>(() => builder.For(exceptionType, PostHandlingAction.None, configure));
        }

        [Fact]
        public void For_Must_Be_Exception_Type()
        {
            var builder = new ExceptionPolicyBuilder(new ServiceCollection().BuildServiceProvider());
            Assert.Throws<ArgumentException>(()=>builder.For(typeof(int), PostHandlingAction.None, _ => { }));
        }

        [Theory]
        [InlineData(null)]
        public void Pre_Arguments_Not_Allow_Null( string configureIndicator)
        {
            Action<IExceptionHandlerBuilder> configure = configureIndicator == null
                ? null as Action<IExceptionHandlerBuilder>
                : _ => { };
            var builder = new ExceptionPolicyBuilder(new ServiceCollection().BuildServiceProvider());
            Assert.Throws<ArgumentNullException>(() => builder.Pre(configure));
        }

        [Theory]
        [InlineData(null)]
        public void Post_Arguments_Not_Allow_Null(string configureIndicator)
        {
            Action<IExceptionHandlerBuilder> configure = configureIndicator == null
                ? null as Action<IExceptionHandlerBuilder>
                : _ => { };
            var builder = new ExceptionPolicyBuilder(new ServiceCollection().BuildServiceProvider());
            Assert.Throws<ArgumentNullException>(() => builder.Post(configure));
        }       

        private static string _flag;
        private class FoobarException : Exception { }
        private class FooException : FoobarException { }
        private class BarException : FoobarException { }
        private class BazException : Exception { }
    }
}
