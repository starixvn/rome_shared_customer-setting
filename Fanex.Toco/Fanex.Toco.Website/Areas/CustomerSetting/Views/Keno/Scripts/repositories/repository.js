"use strict";

KenoModule.factory('kenoRepositories', function ($http, $q, dataService) {
    return {
        //kenoSetting: model,
      kenoSetting: dataService.getKenoSetting(3),
        save: function (url, kenoSetting) {
            var deferred = $q.defer();
            $http.post(url, kenoSetting)
                .success(function (data, status, headers, config) { deferred.resolve(data); })
                .error(function () { deferred.reject(); });
            return deferred.promise;
        }
    }
});

