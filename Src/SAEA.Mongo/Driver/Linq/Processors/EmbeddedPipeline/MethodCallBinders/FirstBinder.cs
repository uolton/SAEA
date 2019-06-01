﻿/* Copyright 2015-present MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SAEA.Mongo.Driver.Linq.Expressions;
using SAEA.Mongo.Driver.Linq.Expressions.ResultOperators;

namespace SAEA.Mongo.Driver.Linq.Processors.EmbeddedPipeline.MethodCallBinders
{
    internal sealed class FirstBinder : IMethodCallBinder<EmbeddedPipelineBindingContext>
    {
        public static IEnumerable<MethodInfo> GetSupportedMethods()
        {
            return MethodHelper.GetEnumerableAndQueryableMethodDefinitions("First");
        }

        public Expression Bind(PipelineExpression pipeline, EmbeddedPipelineBindingContext bindingContext, MethodCallExpression node, IEnumerable<Expression> arguments)
        {
            var source = pipeline.Source;
            if (arguments.Any())
            {
                source = BinderHelper.BindWhere(
                    pipeline,
                    bindingContext,
                    ExpressionHelper.GetLambda(arguments.Single()));
            }

            return new PipelineExpression(
                source,
                pipeline.Projector,
                new FirstResultOperator(
                    node.Type,
                    pipeline.Projector.Serializer,
                    node.Method.Name == nameof(Enumerable.FirstOrDefault)));
        }
    }
}