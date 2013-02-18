module internal TattsApi.Util

open System.Threading

type RetryParams = {
    maxRetries : int; waitBetweenRetries : int
    }

let defaultRetryParams = {maxRetries = 5; waitBetweenRetries = 500}

let rec retry (retryParams:RetryParams) fn = 
    if retryParams.maxRetries > 1 then
        try
            fn()
        with
        | :? System.Net.WebException
        | :? System.Xml.XmlException ->
            Thread.Sleep(retryParams.waitBetweenRetries)
            retry ({maxRetries = retryParams.maxRetries - 1; waitBetweenRetries = retryParams.waitBetweenRetries}) fn
    else
        fn()