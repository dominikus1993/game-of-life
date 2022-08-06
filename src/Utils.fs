namespace GameOfLife

module Utils = 
    let memonize fn = 
        let cache = new System.Collections.Generic.Dictionary<_,_>()
        let f x =
            match cache.TryGetValue x with
            | true, v -> v
            | false, _ -> 
                let value = fn(x)
                cache.Add(x, value)
                value
        f
