var app = angular.module("Homeapp", []);

app.controller("HomeController", function ($scope, $http) {
    $scope.btntext = "Save";
    // Add record
    $scope.savedata = function () {
        $scope.btntext = "Please Wait..";
        $http({
            method: 'POST',
            url: '/Home/AddData',
            data: $scope.EmployeeModel
        }).success(function (d) {
            $scope.btntext = "Save";
            $scope.EmployeeModel = null;
            alert(d);
            window.location.href = "/Home/Index";
        }).error(function () {
            alert('Failed');
        });
    };
    // Display all record
    $http.get("/Home/GetAll").then(function (d) {
        $scope.record = d.data;
    }, function (error) {
        alert('Failed');
    });
    // Display record by id
    $scope.loadrecord = function (empid) {
        $http.get("/Home/GetEmployeeById?empid=" + empid).then(function (d) {
            $scope.btntext = "Update",
                $scope.EmployeeModel = d.data[0];
        }, function (error) {
            alert('Failed');
        });
    };
    // Delete record 
    $scope.deleterecord = function (empid) {
        $http.get("/Home/Delete?empid=" + empid).then(function (d) {
            alert(d.data);
            window.location.href = "/Home/Index";
        }).error(function (d) {
            alert('Failed');
        });
    };
    // Update record
    $scope.updatedata = function () {
        $scope.btntext = "Please Wait..";
        $http({
            method: 'POST',
            url: '/Home/EditRecord',
            data: $scope.EmployeeModel
        }).success(function (d) {
            $scope.btntext = "Update";
            $scope.EmployeeModel = null;
            alert(d);
            window.location.href = "/Home/Index";
        }).error(function () {
            alert('Failed');
        });
    };




    // Excel Functionality
    $scope.SelectedFileForUpload = null;
    $scope.UploadFile = function (files) {
        $scope.$apply(function () { //I have used $scope.$apply because I will call this function from File input type control which is not supported 2 way binding  
            $scope.Message = "";
            $scope.SelectedFileForUpload = files[0];
        })
    }
    //Parse Excel Data   
    $scope.ParseExcelDataAndSave = function () {
        var file = $scope.SelectedFileForUpload;
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = e.target.result;
                //XLSX from js-xlsx library , which I will add in page view page  
                var workbook = XLSX.read(data, { type: 'binary' });
                var sheetName = workbook.SheetNames[0];
                var excelData = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                if (excelData.length > 0) {
                    //Save data   
                    $scope.SaveData(excelData);

                }
                else {
                    $scope.Message = "No data found";
                }
            }
            reader.onerror = function (ex) {
                console.log(ex);
            }

            reader.readAsBinaryString(file);
        }
    }
    // Save excel data to our database  
    $scope.SaveData = function (excelData) {
        $http({
            method: "POST",
            url: "/Home/ImportData",
            data: JSON.stringify(excelData),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (data) {
            if (data.status) {
                $scope.Message = excelData.length + " record inserted";

                window.location.href = "/Home/Index";

            }
            else {
                $scope.Message = "Failed";
            }
        }, function (error) {
            $scope.Message = "Error";
        })
    }


    // Export data to excel
    $scope.export = function () {
        $http.get("/Home/GetAll").then(function (d) {
            $scope.record = d.data;
            alasql('SELECT * INTO XLSX("Employee-List-Export.xlsx",{headers:true}) FROM ?', [$scope.record]);
        }, function (error) {
            alert('Failed');
        });
    };

    // Check Login
    
    $scope.login = function () {
        $scope.btntext = "Please wait...!";
        $http({
            method: "POST",
            url: '/Home/userlogin',
            data:$scope.UserModel
        }).success(function (d) {
            $scope.btntext = 'Login';
            if (d == 1) {
                window.location.href = '/Home/Index';
            }
            else {
                alert('Failed');
            }
            $scope.UserModel = null;
        }).error(function () {
            alert('Failed');
        })
    };
});
