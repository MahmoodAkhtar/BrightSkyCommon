﻿using System;
using System.Threading.Tasks;

namespace BrightSky.Common.Extensions
{
    /// <summary>
    ///     Extentions for async operations where the task appears in the right operand only
    /// </summary>
    public static class AsyncResultExtensionsRightOperand
    {
        public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<T, Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            if (!await predicate(result.Value).ConfigureAwait(continueOnCapturedContext))
                return Result.Fail<T>(errorMessage);

            return Result.Ok(result.Value);
        }

        public static async Task<Result> Ensure(this Result result, Func<Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            if (!await predicate().ConfigureAwait(continueOnCapturedContext))
                return Result.Fail(errorMessage);

            return Result.Ok();
        }

        public static async Task<Result<K>> Map<T, K>(this Result<T> result, Func<T, Task<K>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            K value = await function(result.Value).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> Map<T>(this Result result, Func<Task<T>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            T value = await function().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<T> OnBoth<T>(this Result result, Func<Result, Task<T>> function, bool continueOnCapturedContext = false)
        {
            return await function(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<K> OnBoth<T, K>(this Result<T> result, Func<Result<T>, Task<K>> function, bool continueOnCapturedContext = false)
        {
            return await function(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnFailure<T>(this Result<T> result, Func<Task> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
            {
                await function().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnFailure(this Result result, Func<Task> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
            {
                await function().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<T>> OnFailure<T>(this Result<T> result, Func<string, Task> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
            {
                await function(result.Error).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<K>> OnSuccess<T, K>(this Result<T> result, Func<T, Task<K>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            K value = await function(result.Value).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> OnSuccess<T>(this Result result, Func<Task<T>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            T value = await function().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<K>> OnSuccess<T, K>(this Result<T> result, Func<T, Task<Result<K>>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return await function(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnSuccess<T>(this Result result, Func<Task<Result<T>>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return await function().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<K>> OnSuccess<T, K>(this Result<T> result, Func<Task<Result<K>>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return await function().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> OnSuccess<T>(this Result<T> result, Func<T, Task<Result>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            return await function(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> OnSuccess(this Result result, Func<Task<Result>> function, bool continueOnCapturedContext = false)
        {
            if (result.IsFailure)
                return result;

            return await function().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnSuccess<T>(this Result<T> result, Func<T, Task> action, bool continueOnCapturedContext = false)
        {
            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnSuccess(this Result result, Func<Task> action, bool continueOnCapturedContext = false)
        {
            if (result.IsSuccess)
            {
                await action().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }
    }
}