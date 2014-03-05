angular.module('kenoStatusApp', []);

angular.element($('#ng-app')).ready(function () {
    $('#ng-app').show();
});

function KenoStatusController($scope) {
    $scope.model = model;

    $scope.sportTypes = $scope.model.settings.sportTypes;

    $scope.CheckStatus = function (preVal, scope) {
        if (preVal) {
            if (scope.item.enable) {
                scope.item.enableDownline = true;
                scope.item.disableDownline = true;
            }
            else {
                scope.item.enableDownline = false;
                scope.item.disableDownline = true;
            }
        }
        else {
            if (scope.item.enable) {
                scope.item.enableDownline = false;
                scope.item.disableDownline = false;
            }
            else {
                scope.item.enableDownline = false;
                scope.item.disableDownline = true;
            }
        }
    };

    $scope.Update = function () {
        //parent.Fanex.ExtAcc.BlockUI();

        $.ajax({
            dataType: 'json',
            type: 'post',
            url: resolveClientUrl('CustomerSetting/Keno/EnableStatus/Update'),
            data: {
                id: custId,
                uplineId: uplineId,
                txtCustomerObject: JSON.stringify(model),
                roleId: roleId,
                groupName: ''
            }
        })
        .success(function (data) {                        
            var errorCodes = data.errorCode.toString().indexOf('\u0007') != -1 ? data.errorCode.split('\u0007') : data.errorCode.toString();
            var success = true;

            $(errorCodes).each(function (i, error) {
                if (error != '' && parseInt(error) >= 0) {
                    success = false;
                }
            });

            if (data.errorCode == "0" || data.errorCode == "" || success) {
                $('#errMsg').attr('class', 'successMsg');
                $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                $('#errMsg').show();

                var timeout = setTimeout(function () {
                    clearTimeout(timeout);

                    document.location.reload(true);
                }, 3000);
            }
            else {
                $('#errMsg').attr('class', 'errMsg');
                $('#errMsg').html(data.errorMessage.formatErrorMessage());
                $('#errMsg').show();
            }

            //parent.Fanex.ExtAcc.UnBlockUI();
        }).error(function (data) {
            if (typeof console != 'undefined')
                console.log(data);            

            //parent.Fanex.ExtAcc.UnBlockUI();
        });
    };
};
