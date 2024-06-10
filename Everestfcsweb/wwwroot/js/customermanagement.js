function Savesystempermissiondetails() {
    document.getElementById("saveUpdateSystemPermissions").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#Systempermissionnameid').val() == '') {
        Swal.fire("Permission Not Created", 'Permission Name is Required', "warning");
        document.getElementById("saveUpdateSystemPermissions").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    var uil1 = {
        PermissionId: $('#Systempermissionid').val(), Permissionname: $('#Systempermissionnameid').val(), Isadmin: $('#Systempermissionisadminid').is(':checked')
    };
    $.post("/SettingsManagement/Addsystempermissiondata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Permission details not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("saveUpdateSystemPermissions").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}
function unauthsavesystemtenantaccountsettingdata() {
    document.getElementById("RegisterSystemTenantsDataBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    var uil =
    {
        Tenantid: $('#SystemTenantAccountid').val(), Countryid: $('#SystemTenantAccountCountryid').val(), Tenantname: $('#SystemTenantAccountTenantnameid').val(), Tenantsubdomain: $('#SystemTenantAccountTenantSubdomainid').val(), TenantLogo: $('#UttambsolutionsimagesurlId').val().replace(/^\[|\]$/g, '').replace(/^"|"$/g, ''), TenantEmail: $('#SystemTenantAccountTenantEmailid').val(), Phoneid: $('#inputGroup-sizing-sm').val(), Phonenumber: $('#PhonenumberId').val(),
        TenantReference: $('#SystemTenantAccountTenantReferenceid').val(), TenantPIN: $('#SystemTenantAccountTenantpinid').val(), IsCCEmail: $('#SystemTenantAccountIsccemailid').is(':checked'), CCEmail: $('#SystemTenantAccountCcEmailid').val(), StaffAutoLogOff: true, EmailAddress: $('#SystemTenantAccountEmailAddressid').val(), EmailPassword: $('#SystemTenantAccountEmailPasswordid').val(),
        Messageusername: $('#SystemTenantAccountMessageUsernameid').val(), Messageapikey: $('#SystemTenantAccountMessageApiKeyid').val(), ApplyTax: $('#SystemTenantAccountApplyTaxid').is(':checked'), NoOfDecimalPlaces: $('#SystemTenantAccountNoofDecimalPlacesid').val(), IsEmailEnabled: $('#SystemTenantAccountIsEmailEnabledid').is(':checked'), IsSmsEnabled: $('#SystemTenantAccountIsSmsEnabledid').is(':checked'),
        IsTemplateTrancated: $('#SystemTenantAccountIsTemplateTrancatedid').is(':checked'), Extra: $('#SystemTenantAccountExtraid').val(), Extra1: $('#SystemTenantAccountExtra1id').val(), Extra2: $('#SystemTenantAccountExtra2id').val(), Extra3: $('#SystemTenantAccountExtra3id').val(), Extra4: $('#SystemTenantAccountExtra4id').val(), Extra5: $('#SystemTenantAccountExtra5id').val(), Extra6: $('#SystemTenantAccountExtra6id').val(),
        Extra7: $('#SystemTenantAccountExtra7id').val(), Extra8: $('#SystemTenantAccountExtra8id').val(), Extra9: $('#SystemTenantAccountExtra9id').val(), Extra10: $('#SystemTenantAccountExtra10id').val(), Tenantloginstatus: $('#SystemTenantAccountloginStatusid').val(), Createdby: $('#systemLoggedinedTenantid').val(), Modifiedby: $('#systemLoggedinedTenantid').val(), DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/SettingsManagement/Unauthregistertenantaccountdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            window.location.href = '/Account/Signinuser';
            /* setTimeout(function () { location.reload(); }, 1000);*/
        } else if (response.RespStatus == 1) {
            Swal.fire("Tenant Settings details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("RegisterSystemTenantsDataBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "REGISTER";
    });
}
function savesystemtenantaccountsettingdata() {
    document.getElementById("SaveSystemTenantsDataBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    var uil =
    {
        Tenantid: $('#SystemTenantAccountid').val(), Countryid: $('#SystemTenantAccountCountryid').val(), Tenantname: $('#SystemTenantAccountTenantnameid').val(), Tenantsubdomain: $('#SystemTenantAccountTenantSubdomainid').val(), TenantLogo: $('#UttambsolutionsimagesurlId').val().replace(/^\[|\]$/g, '').replace(/^"|"$/g, ''), TenantEmail: $('#SystemTenantAccountTenantEmailid').val(), Phoneid: $('#inputGroup-sizing-sm').val(), Phonenumber: $('#PhonenumberId').val(),
        TenantReference: $('#SystemTenantAccountTenantReferenceid').val(), TenantPIN: $('#SystemTenantAccountTenantpinid').val(), IsCCEmail: $('#SystemTenantAccountIsccemailid').is(':checked'), CCEmail: $('#SystemTenantAccountCcEmailid').val(), StaffAutoLogOff: true, EmailAddress: $('#SystemTenantAccountEmailAddressid').val(), EmailPassword: $('#SystemTenantAccountEmailPasswordid').val(),
        Messageusername: $('#SystemTenantAccountMessageUsernameid').val(), Messageapikey: $('#SystemTenantAccountMessageApiKeyid').val(), ApplyTax: $('#SystemTenantAccountApplyTaxid').is(':checked'), NoOfDecimalPlaces: $('#SystemTenantAccountNoofDecimalPlacesid').val(), IsEmailEnabled: $('#SystemTenantAccountIsEmailEnabledid').is(':checked'), IsSmsEnabled: $('#SystemTenantAccountIsSmsEnabledid').is(':checked'),
        IsTemplateTrancated: $('#SystemTenantAccountIsTemplateTrancatedid').is(':checked'), Extra: $('#SystemTenantAccountExtraid').val(), Extra1: $('#SystemTenantAccountExtra1id').val(), Extra2: $('#SystemTenantAccountExtra2id').val(), Extra3: $('#SystemTenantAccountExtra3id').val(), Extra4: $('#SystemTenantAccountExtra4id').val(), Extra5: $('#SystemTenantAccountExtra5id').val(), Extra6: $('#SystemTenantAccountExtra6id').val(),
        Extra7: $('#SystemTenantAccountExtra7id').val(), Extra8: $('#SystemTenantAccountExtra8id').val(), Extra9: $('#SystemTenantAccountExtra9id').val(), Extra10: $('#SystemTenantAccountExtra10id').val(), Tenantloginstatus: $('#SystemTenantAccountloginStatusid').val(), Createdby: $('#systemLoggedinedTenantid').val(), Modifiedby: $('#systemLoggedinedTenantid').val(), DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/SettingsManagement/Registertenantaccountdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Tenant Settings details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("SaveSystemTenantsDataBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}

function Savesystemsupplierdetaildata() {
    document.getElementById("SaveSystemSupplierDetailBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#SystemTenantSupplierNameid').val() == '') {
        Swal.fire("Suplier details Not saved", 'Supplier Name is Required', "warning");
        return;
    }
    if ($('#SystemTenantSupplierEmailid').val() == '') {
        Swal.fire("Suplier details Not saved", 'Supplier Email is Required', "warning");
        return;
    }

    if ($('#inputGroup-sizing-sm').val() == '' || $('#inputGroup-sizing-sm').val() == 0) {
        Swal.fire("Suplier details Not saved", 'Supplier Phone Code is Required', "warning");
        return;
    }
    if ($('#PhonenumberId').val() == '') {
        Swal.fire("Suplier details Not saved", 'Supplier Phone is Required', "warning");
        return;
    }
    var SystemSuplierPricesData = [];

    $("#SuplierProductsTable tbody tr").each(function () {
        var row = $(this);
        // Extract data from input fields within each row
        var SystemSuplierPriceData = {
            SupplierPriceId: row.find('td:eq(0)').text(),
            SupplierId: row.find('td:eq(1)').text(),
            ProductVariationId: row.find('td:eq(2)').text(),
            ProductPrice: row.find('td:eq(4)').text().replace(/,/g, ''),
            ProductVat: row.find('td:eq(5)').text().replace(/,/g, ''),
            Createdby: $('#systemLoggedinedUserid').val(),
            Modifiedby: $('#systemLoggedinedUserid').val(),
            DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
        };
        SystemSuplierPricesData.push(SystemSuplierPriceData);
    });
    var uil = {};
    if (SystemSuplierPricesData.length > 0) {
        uil =
        {
            Tenantid: $('#systemLoggedinedTenantid').val(), SupplierId: $('#SupplierId').val(), SupplierName: $('#SystemTenantSupplierNameid').val(), SupplierEmail: $('#SystemTenantSupplierEmailid').val(), PhoneId: $('#inputGroup-sizing-sm').val(), PhoneNumber: $('#PhonenumberId').val(),
            Extra: $('#ExtraId').val(), Extra1: $('#Extra1Id').val(), Extra2: $('#Extra2Id').val(), Extra3: $('#Extra3Id').val(), Extra4: $('#Extra4Id').val(), Extra5: $('#Extra5Id').val(), Extra6: $('#Extra6Id').val(), Extra7: $('#Extra7Id').val(), Extra8: $('#Extra8Id').val(), Extra9: $('#Extra9Id').val(), Extra10: $('#Extra10Id').val(),
            Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            SystemSuplierPrice: SystemSuplierPricesData,
        }
    }else {
        Swal.fire('"Suplier details Not saved', 'Some data are empty', 'warning')
        document.getElementById("SaveSystemSupplierDetailBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    $.post("/SettingsManagement/Addsystemsupplierdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Suplier details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("SaveSystemSupplierDetailBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}
function savesystemloyaltysettingdata() {
    document.getElementById("saveUpdateLoyaltySettings").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    var uil =
    {
        TenantId: $('#SystemTenantId').val(), LoyaltysettId: $('#LoyaltysettId').val(), NumberFormat: $('#NumberFormatId').val(), RoundofDecimals: $('#RoundofDecimalsId').val(), CeilorFloor: $('#CeilorFloorId').val(),
        CollisionRule: $('#CollisionRuleId').val(), NoOfTransactionPerDay: $('#NoOfTransactionPerDayId').val(), AmountPerDay: $('#AmountPerDayId').val(), ConsecutiveTransTimeMin: $('#ConsecutiveTransTimeMinId').val(),
        MinRedeemPoints: $('#MinRedeemPointsId').val(), FromRewardId: $('#FromRewardId').val(), ToRewardId: $('#ToRewardId').val(), ConversionValue: $('#ConversionValueId').val(),
        AutoRedeem: $('#AutoredeemId').is(':checked'), RedeemPeriod: $('#RedeemPeriodId').val(), RedeemDay: $('#RedeemDayId').val(), CreatedbyId: $('#systemLoggedinedUserid').val(), ModifiedId: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/SettingsManagement/AddsystemloyaltysettingData", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Loyalty Settings details has been saved.', 'success')
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Loyalty Settings details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("saveUpdateLoyaltySettings").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}




function savetransferproducttoaccessory() {
    document.getElementById("savetransferproducttoaccessorybtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#ProductquantitytotransferId').val() == '' || $('#ProductquantitytotransferId').val() == 0) {
        Swal.fire("Transfer Not Done", 'Quantity is Required', "warning");
        document.getElementById("savetransferproducttoaccessorybtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "MAKE A TRANSFER";
        return;
    }
    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), DryStockStoreId: $('#DryStockStoreId').val(), ProductvariationId: $('#ProductvariationId').val(), Quantity: $('#ProductquantitytotransferId').val(), Movement:'Transfertoaccessories', Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/ProductManagement/Savetransfertoaccessoriesdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage , 'success')
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Transfer Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("savetransferproducttoaccessorybtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "MAKE A TRANSFER";
    });
}
function savetransferproductfromaccessory() {
    document.getElementById("savetransferproductfromaccessorybtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#ProductquantitytotransferId').val() == '' || $('#ProductquantitytotransferId').val() == 0) {
        Swal.fire("Transfer Not Done", 'Quantity is Required', "warning");
        document.getElementById("savetransferproductfromaccessorybtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "MAKE A TRANSFER";
        return;
    }
    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), DryStockStoreId: $('#DryStockStoreId').val(), ProductvariationId: $('#ProductvariationId').val(), Quantity: $('#ProductquantitytotransferId').val(), Movement: 'Transferfromaccessories', Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/ProductManagement/Savetransfertoaccessoriesdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Transfer Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("savetransferproducttoaccessorybtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "MAKE A TRANSFER";
    });
}
function Savesystemcommunicationtemplatedata() {
    if ($('#SystemcommtemplatemoduleId').val() == '' || $('#SystemcommtemplatemoduleId').val() == 0) {
        Swal.fire("Communication Template Not Created", 'Module is Required', "warning");
        return;
    }
    var uil1 = {
        Templateid: $('#SystemcommTemplateId').val(), Templatename: $('#SystemcommtemplatenameId').val(), Templatesubject: $('#SystemcommtemplatesubjectId').val(),
        Isemailsms: $('#SystemcommtemplateisemailsmsId').is(':checked'), Templatebody: $('#SystemcommtemplatebodyId').val()
    };
    $.post("/CommunicationTemplate/Addcommunicationtemplatedata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Communication templates details not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savesystemcustomerdata() {
    document.getElementById("SaveSystemCustomerDataBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#customerDesgnationId').val() === "Individual") {
        if ($('#firstnameId').val() == '') {
            Swal.fire("Customer Detail Not Created", 'Firstname is Required', "warning");
            document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
            document.getElementById("processingSpinner").style.display = "none";
            document.getElementById("buttonText").innerText = "SAVE";
            return;
        }
        if ($('#lastnameId').val() == '') {
            Swal.fire("Customer Detail Not Created", 'Lastname is Required', "warning");
            document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
            document.getElementById("processingSpinner").style.display = "none";
            document.getElementById("buttonText").innerText = "SAVE";
            return;
        }
    } else {
        if ($('#companynameId').val() == '') {
            Swal.fire("Customer Detail Not Created", 'Company Name is Required', "warning");
            document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
            document.getElementById("processingSpinner").style.display = "none";
            document.getElementById("buttonText").innerText = "SAVE";
            return;
        }
    }

    if ($('#emailaddressId').val() == '') {
        Swal.fire("Customer Detail Not Created", 'Emailaddress is Required', "warning");
        document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#inputGroup-sizing-sm').val() == '' || $('#inputGroup-sizing-sm').val() == 0) {
        Swal.fire("Customer Detail Not Created", 'Phonenumber Prefix is Required', "warning");
        document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#PhonenumberId').val() == '') {
        Swal.fire("Customer Detail Not Created", 'Phonenumber is Required', "warning");
        document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#CustomerstationId').val() == '' || $('#CustomerstationId').val() == 0) {
        Swal.fire("Customer Detail Not Created", 'Station is Required', "warning");
        document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }

    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), CustomerId: $('#customerId').val(), Firstname: $('#firstnameId').val(), Lastname: $('#lastnameId').val(), Companyname: $('#companynameId').val(),
        Designation: $('#customerDesgnationId').val(), Emailaddress: $('#emailaddressId').val(), Phoneid: $('#inputGroup-sizing-sm').val(), Phonenumber: $('#PhonenumberId').val(), StationId: $('#CustomerstationId').val(),
        IDNumber: $('#customeridnumberId').val(), CompanyPIN: $('#CompanypinId').val(), CompanyIncorporationDate: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), CompanyVAT: $('#CompanyVATNoId').val(), CountryId: $('#CustomercountryId').val(),
        NoOfTransactionPerDay: $('#NoOfTransactionPerDayId').val(), AmountPerDay: $('#AmountPerDayId').val(), ConsecutiveTransTimeMin: $('#ConsecutiveTransTimeMinId').val(), CompanyAddress: $('#CompanyaddressId').val(),
        CompanycontactpersonnameId: $('#CompanycontactpersonnameId').val(), CompanycontactpersonemailId: $('#CompanycontactpersonemailId').val(), CompanycontactpersonphoneId: $('#CompanycontactpersonphoneId').val(),
        CompanyRegistrationNo: $('#CompanyRegistrationNoId').val(), IsPortaluser: true,
        Dob: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Gender: $('#customergenderId').val(), ReferenceNumber: $('#customerreferencenumberId').val(),
        Contractstartdate: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
        Contractenddate: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addsystemcustomer", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Customer details has been added.', 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Customer details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("SaveSystemCustomerDataBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}
function savecustomerprepaidagreementdetails() {
    if ($('#LoyaltyGroupingId').val() == '' || $('#LoyaltyGroupingId').val() == 0) {
        Swal.fire("Prepaid Detail Not Created", 'Loyalty Group is Required', "warning");
        return;
    }
    if ($('#PricelistId').val() == '' || $('#PricelistId').val() == 0) {
        Swal.fire("Prepaid Detail Not Created", 'Pricelist is Required', "warning");
        return;
    }
    if ($('#DiscountlistId').val() == '' || $('#DiscountlistId').val() == 0) {
        Swal.fire("Prepaid Detail Not Created", 'Discountlist is Required', "warning");
        return;
    }
    var uil1 = {
        AgreementId: $('#AgreementId').val(), AgreementType: $('#AgreementTypeId').val(),
        CustomerId: $('#CustomerId').val(), GroupingId: $('#LoyaltyGroupingId').val(), PricelistId: $('#PricelistId').val(), DiscountlistId: $('#DiscountlistId').val(),
        AgreementDoc: $('#PostAgreementDocId').val(), Descriptions: $('#AgreementnoteId').val(), Notes: $('#AgreementnoteId').val(), HasGroup: 0, HasOverdraft: 0,
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addsystemcustomerprepaidagreementdata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Prepaid Agreement details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Prepaid Agreement details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savecustomerpostpaidcreditagreementdetails() {
    if ($('#PostpaidcreditLoyaltyGroupingId').val() == '' || $('#PostpaidcreditLoyaltyGroupingId').val() == 0) {
        Swal.fire("Postpaid Credit Detail Not Created", 'Loyalty Group is Required', "warning");
        return;
    }
    if ($('#PostpaidcreditPricelistId').val() == '' || $('#PostpaidcreditPricelistId').val() == 0) {
        Swal.fire("Postpaid Credit Detail Not Created", 'Pricelist is Required', "warning");
        return;
    }
    if ($('#PostpaidcreditDiscountlistId').val() == '' || $('#PostpaidcreditDiscountlistId').val() == 0) {
        Swal.fire("Postpaid Credit Detail Not Created", 'Discountlist is Required', "warning");
        return;
    }
    if ($('#PostpaidcreditConsumptionLimitTypeId').val() == '' || $('#PostpaidcreditConsumptionLimitTypeId').val() == 0) {
        Swal.fire("Postpaid Credit Detail Not Created", 'Limit Type is Required', "warning");
        return;
    }
    if ($('#PostpaidcreditLimitValueId').val() == '') {
        Swal.fire("Postpaid Credit Detail Not Created", 'Limit Value is Required', "warning");
        return;
    }
    if ($('#PostpaidcreditPaymentTermDaysId').val() == '' || $('#PostpaidcreditPaymentTermDaysId').val() == 0) {
        Swal.fire("Postpaid Credit Detail Not Created", 'Payment Terms is Required', "warning");
        return;
    }
    if ($('#PostpaidcreditAgreementReferenceId').val() == '' || $('#PostpaidcreditAgreementReferenceId').val() == 0) {
        Swal.fire("Postpaid Credit Detail Not Created", 'Agreement Reference is Required', "warning");
        return;
    }
    var uil1 = {
        AgreementId: $('#AgreementId').val(), AgreementType: $('#AgreementTypeId').val(),
        CustomerId: $('#CustomerId').val(), GroupingId: $('#PostpaidcreditLoyaltyGroupingId').val(), PricelistId: $('#PostpaidcreditPricelistId').val(), DiscountlistId: $('#PostpaidcreditDiscountlistId').val(), ConsumptionLimitType: $('#PostpaidcreditConsumptionLimitTypeId').val(),
        LimitValue: $('#PostpaidcreditLimitValueId').val(), PaymentTerms: $('#PostpaidcreditPaymentTermDaysId').val(), AgreementReference: $('#PostpaidcreditAgreementReferenceId').val(), AgreementDoc: $('#PostpaidcreditAgreementDocId').val(), Descriptions: $('#PostpaidcreditAgreementnoteId').val(), Notes: $('#PostpaidcreditAgreementnoteId').val(), HasGroup: 0, HasOverdraft: 0,
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addsystemcustomerpostpaidcreditagreementdata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Postpaid Credit  Agreement details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Postpaid Credit Agreement details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savecustomerpostpaidrecurrentagreementdetails() {
    if ($('#PostpaidrecurrentLoyaltyGroupingId').val() == '' || $('#PostpaidrecurrentLoyaltyGroupingId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Loyalty Group is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentPricelistId').val() == '' || $('#PostpaidrecurrentPricelistId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Pricelist is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentDiscountlistId').val() == '' || $('#PostpaidrecurrentDiscountlistId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Discountlist is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentbillincycleTypeId').val() == '' || $('#PostpaidrecurrentbillincycleTypeId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Billing Cycle is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentConsumptionLimitTypeId').val() == '' || $('#PostpaidrecurrentConsumptionLimitTypeId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Limit Type is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentLimitValueId').val() == '') {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Limit Value is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentbillingbasisId').val() == '' || $('#PostpaidrecurrentbillingbasisId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Billing Basis is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentstartdateId').val() == '') {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Start Date is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentgracePeriodId').val() == '' || $('#PostpaidrecurrentgracePeriodId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Grace Period is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentAgreementReferenceId').val() == '' || $('#PostpaidrecurrentAgreementReferenceId').val() == 0) {
        Swal.fire("Postpaid Recurrent Detail Not Created", 'Agreement Refence is Required', "warning");
        return;
    }
    if ($('#PostpaidrecurrentbillincycleTypeId').val() == 'Weekly') {
        if ($('#PostpaidrecurrentbillingcycleId').val() == '' || $('#PostpaidrecurrentbillingcycleId').val() == 0) {
            Swal.fire("Postpaid Recurrent Detail Not Created", 'Billing Cycle is Required', "warning");
            return;
        }
    } else {
        if ($('#PostpaidrecurrentbillingdayId').val() == '' || $('#PostpaidrecurrentbillingdayId').val() == 0) {
            Swal.fire("Postpaid Recurrent Detail Not Created", 'Billing Day is Required', "warning");
            return;
        }
    }
    var uil1 = {
        AgreementId: $('#AgreementId').val(), AgreementType: $('#AgreementTypeId').val(),
        CustomerId: $('#CustomerId').val(), GroupingId: $('#PostpaidrecurrentLoyaltyGroupingId').val(), PriceListId: $('#PostpaidrecurrentPricelistId').val(), DiscountListId: $('#PostpaidrecurrentDiscountlistId').val(), BillingCycleType: $('#PostpaidrecurrentbillincycleTypeId').val(), ConsumptionLimitType: $('#PostpaidrecurrentConsumptionLimitTypeId').val(),
        ConsumptionLimitValue: $('#PostpaidrecurrentLimitValueId').val(), Descriptions: $('#PostpaidrecurrentAgreementdescriptionId').val(), Reference: $('#PostpaidrecurrentAgreementReferenceId').val(), AgreementDoc: $('#PostpaidrecurrentAgreementDocId').val(), BillingBasis: $('#PostpaidrecurrentbillingbasisId').val(), StartDate: $('#PostpaidrecurrentstartdateId').val(),
        GracePeriod: $('#PostpaidrecurrentgracePeriodId').val(), BillingDay: $('#PostpaidrecurrentbillingdayId').val(), BillingCycle: $('#PostpaidrecurrentbillingcycleId').val(), HasGroup: 0, HasOverdraft: 0,
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addsystemcustomerpostpaidrecurrentagreementdata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Postpaid Recurrent  Agreement details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Postpaid Recurrent Agreement details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savecustomerpostpaidoneoffagreementdetails() {
    if ($('#PostpaidoneoffLoyaltyGroupingId').val() == '' || $('#PostpaidoneoffLoyaltyGroupingId').val() == 0) {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Loyalty Group is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffPricelistId').val() == '' || $('#PostpaidoneoffPricelistId').val() == 0) {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Pricelist is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffDiscountlistId').val() == '' || $('#PostpaidoneoffDiscountlistId').val() == 0) {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Discountlist is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffbillingbasislistId').val() == '' || $('#PostpaidoneoffbillingbasislistId').val() == 0) {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Billing Basis is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffConsumptionLimitTypeId').val() == '' || $('#PostpaidoneoffConsumptionLimitTypeId').val() == 0) {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Limit Type is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffConsumptionLimitvalueId').val() == '') {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Limit Value is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffstartdateId').val() == '') {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Start Date is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffenddateId').val() == '') {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'End Date is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffgraceperiodId').val() == '' || $('#PostpaidoneoffgraceperiodId').val() == 0) {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Grace Period is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffagreementreferenceId').val() == '' || $('#PostpaidoneoffagreementreferenceId').val() == 0) {
        Swal.fire("Postpaid Oneoff Detail Not Created", 'Agreement Refence is Required', "warning");
        return;
    }
    if ($('#PostpaidoneoffbillingbasislistId').val() == 'Fixed') {
        if ($('#PostpaidoneoffbillingamountId').val() == '' || $('#PostpaidoneoffbillingamountId').val() == 0) {
            Swal.fire("Postpaid Oneoff Detail Not Created", 'Billing Cycle is Required', "warning");
            return;
        }
    }
    var uil1 = {
        AgreementId: $('#AgreementId').val(), AgreementType: $('#AgreementTypeId').val(),
        CustomerId: $('#CustomerId').val(), GroupingId: $('#PostpaidoneoffLoyaltyGroupingId').val(), PriceListId: $('#PostpaidoneoffPricelistId').val(), DiscountListId: $('#PostpaidoneoffDiscountlistId').val(), BillingBasis: $('#PostpaidoneoffbillingbasislistId').val(), ConsumptionLimitType: $('#PostpaidoneoffConsumptionLimitTypeId').val(),
        ConsumptionLimitValue: $('#PostpaidoneoffConsumptionLimitvalueId').val(), Descriptions: $('#PostpaidoneoffagreementdescriptionId').val(), Reference: $('#PostpaidoneoffagreementreferenceId').val(), AgreementDoc: $('#PostpaidoneoffAgreementDocId').val(), StartDate: $('#PostpaidoneoffstartdateId').val(),
        GracePeriod: $('#PostpaidoneoffgraceperiodId').val(), EndDate: $('#PostpaidoneoffenddateId').val(), BillingAmount: $('#PostpaidoneoffbillingamountId').val(), HasGroup: 0, HasOverdraft: 0,
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addsystemcustomerpostpaidoneoffagreementdata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Postpaid Oneoff  Agreement details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Postpaid Oneoff Agreement details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savecustomeragreementaccountdetails() {
    document.getElementById("SaveCustomerAccountBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#SystemcardtypeId').val() == "" || $('#SystemcardtypeId').val() == 0) {
        Swal.fire("Account Detail Not Created", 'Card Type is Required', "warning");
        document.getElementById("SaveCustomerAccountBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#SystemcardmaskId').val() == "" || $('#SystemcardmaskId').val() == 0) {
        Swal.fire("Account Detail Not Created", 'Card Mask is Required', "warning");
        document.getElementById("SaveCustomerAccountBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#CustomerAgreementAccountLimitPeriodId').val() == "" || $('#CustomerAgreementAccountLimitPeriodId').val() == "0") {
        Swal.fire("Account Detail Not Created", 'Card Limit Period is Required', "warning");
        document.getElementById("SaveCustomerAccountBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#CustomerAgreementAccountLimitTypeId').val() == "" || $('#CustomerAgreementAccountLimitTypeId').val() == "0") {
        Swal.fire("Account Detail Not Created", 'Card Limit Type is Required', "warning");
        return;
    }
    if ($('#CustomerAgreementAccountLimitTypeValueId').val() == "" || $('#CustomerAgreementAccountLimitTypeValueId').val() == 0) {
        Swal.fire("Account Detail Not Created", 'Limit Value is Required', "warning");
        document.getElementById("SaveCustomerAccountBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    var uil1 = {
        AgreementId: $('#CustomerAgreementId').val(), CreateAccountType: $('#CustomerCreateAccountTypeId').val(), MaskType: $('#SystemcardtypeId').val(),
        MaskSno: $('#CustomerAgreementAccountMaskSnoId').val(), MaskId: $('#SystemcardmaskId').val(), ConsumptionPeriod: $('#CustomerAgreementAccountLimitPeriodId').val(), LimitTypeId: $('#CustomerAgreementAccountLimitTypeId').val(), ConsumptionLimit: $('#CustomerAgreementAccountLimitTypeValueId').val(),
        EquipmentReg: $('#EquipmentRegistrationId').val(), VehicleMakeId: $('#EquipmentMakeId').val(), VehicleModelId: $('#EquipmentModelId').val(), ProductVariationId: $('#EquipmentProductId').val(), TankCapacity: $('#TankCapacityId').val(), OdometerReading: $('#OdometerReadingId').val(),
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/AddcustomeragreementAccountdata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Customer Agreement Account details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Customer Agreement Account details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("SaveCustomerAccountBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}
function saveaccountcardreplacedetails() {
    if ($('#SystemcardtypeId').val() == "" || $('#SystemcardtypeId').val() == 0) {
        Swal.fire("Account Detail Not Created", 'Card Type is Required', "warning");
        return;
    }
    if ($('#SystemcardmaskId').val() == "" || $('#SystemcardmaskId').val() == 0) {
        Swal.fire("Account Detail Not Created", 'Card Mask is Required', "warning");
        return;
    }
    var uil1 = {
        AccountId: $('#CustomerAccountId').val(), CardId: $('#CustomerCardId').val(), MaskType: $('#SystemcardtypeId').val(),
        MaskSno: $('#CustomerAgreementAccountMaskSnoId').val(), MaskId: $('#SystemcardmaskId').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Replacecustomeraccountcarddata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Account Card Mask Replaced.', 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Account Card Mask Not Replaced", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savecustomeragreementaccounttopup() {
    var uil =
    {
        AccountId: $('#TopupAccountId').val(), TenantId: $('#systemLoggedinedTenantid').val(), PaymentModeId: $('#PaymentModeId').val(), TopupAmount: $('#TopupAmountId').val(), TopupDatecreated: $('#TopupDatecreatedId').val(), TopupReference: $('#TopupReferenceId').val(), StationId: $('#SystemStationId').val(),
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };

    $.post("/CustomerManagement/AddcustomeragreementAccounttopupdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Topup details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Topup details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}
function reversetopupthis(event) {
    Swal.fire({
        title: "Are you sure you want to revese this?",
        text: "Once reversed, One have to post another topup!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Reverse it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var removeBtn = event.target;
            var Entryid = removeBtn.getAttribute('data-FinanceTransactionId');
            var uil =
            {
                FinanceTransactionId: Entryid,
                StaffId: $('#systemLoggedinedUserid').val(),
                Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
            };
            $.post("/CustomerManagement/Reversetopup", uil, function (response) {
                if (response.RespStatus == 0) {
                    Swal.fire('Item Reversed!', response.RespMessage, 'success')
                    setTimeout(function () { location.reload(); }, 1000);
                }
                else if (response.RespStatus == 1) {
                    Swal.fire("Oops!", response.RespMessage, "warning");
                }
                else {
                    Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
                }

            });
        } else {
            Swal.fire("Thank you!", "Reverse once you are sure", "info");
        }
    });
};
function reversepaymentthis(event) {
    Swal.fire({
        title: "Are you sure you want to revese this?",
        text: "Once reversed, One have to post another Payment!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Reverse it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var removeBtn = event.target;
            var Entryid = removeBtn.getAttribute('data-FinanceTransactionId');
            var uil =
            {
                FinanceTransactionId: Entryid,
                StaffId: $('#systemLoggedinedUserid').val(),
                Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
            };
            $.post("/CustomerManagement/Reversepayment", uil, function (response) {
                if (response.RespStatus == 0) {
                    Swal.fire('Item Reversed!', response.RespMessage, 'success')
                    setTimeout(function () { location.reload(); }, 1000);
                }
                else if (response.RespStatus == 1) {
                    Swal.fire("Oops!", response.RespMessage, "warning");
                }
                else {
                    Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
                }

            });
        } else {
            Swal.fire("Thank you!", "Reverse once you are sure", "info");
        }
    });
};
function savecustomeragreementaccounttransfer() {
    if ($('#TransferToAccountId').val() == "" || $('#TransferToAccountId').val() == "") {
        Swal.fire("Transfer Detail Not Created", 'Account To is Required', "warning");
        return;
    }
    if ($('#TransferAmountId').val() == "" || $('#TransferAmountId').val() == 0) {
        Swal.fire("Transfer Detail Not Created", 'Amount To Transfer is Required', "warning");
        return;
    }
    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), , FromAccountId: $('#TransferFromAccountId').val(), ToAccountId: $('#TransferToAccountId').val(), TransferAmount: $('#TransferAmountId').val(),
        CreatedbyId: $('#systemLoggedinedUserid').val(), ModifiedId: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };

    $.post("/CustomerManagement/AddcustomeragreementAccountTransferdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Topup details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Topup details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}

function savecustomeragreementpayment() {
    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), AgreementId: $('#CustomeragreementId').val(), AgreementType: $('#CustomerAgreementTypeId').val(), AgreementLpo: $('#CustomerAgreementLpoId').val(), PaymentModeId: $('#PaymentModeId').val(), Amount: $('#CustomerPaidAmountId').val(), TransactionDate: $('#PaymentDatecreatedId').val(), TransactionReference: $('#PaymentReferenceId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };

    $.post("/CustomerManagement/Addcustomeragreementpaymentdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Payment details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Payment details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}

function savecustomeragreementaccountLeveltopup() {
    if ($('#PaymentModeId').val() == '' || $('#PaymentModeId').val() == 0) {
        Swal.fire("Agreement Topup Detail Not Created", 'Paymentmode is Required', "warning");
        return;
    }
    if ($('#TopupAmountId').val() == '' || $('#TopupAmountId').val() == 0) {
        Swal.fire("Agreement Topup Detail Not Created", 'Amount is Required', "warning");
        return;
    }
    if ($('#TopupDatecreatedId').val() == '') {
        Swal.fire("Agreement Topup Detail Not Created", 'Date Created is Required', "warning");
        return;
    }
    if ($('#TopupReferenceId').val() == '') {
        Swal.fire("Agreement Topup Detail Not Created", 'Topup Refence is Required', "warning");
        return;
    }
    if ($('#SystemStationId').val() == '' || $('#SystemStationId').val() == 0) {
        Swal.fire("Agreement Topup Detail Not Created", 'Station is Required', "warning");
        return;
    }
    if ($('#SystemstationstaffId').val() == '' || $('#SystemstationstaffId').val() == 0) {
        Swal.fire("Agreement Topup Detail Not Created", 'Station Staff is Required', "warning");
        return;
    }
    var uil =
    {
        AccountId: $('#TopupAccountId').val(), TenantId: $('#systemLoggedinedTenantid').val(), PaymentModeId: $('#PaymentModeId').val(), TopupAmount: $('#TopupAmountId').val(), TopupDatecreated: $('#TopupDatecreatedId').val(), TopupReference: $('#TopupReferenceId').val(), StationId: $('#SystemStationId').val(), StaffId: $('#SystemstationstaffId').val(),
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/CustomerManagement/AddcustomeragreementAccounttopupdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Topup details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
            //$.ajax({
            //    url: "/CustomerManagement/Customeraccountdetail?AccountId=" + $('#EmployeeAccountId').val(),
            //    type: "GET",
            //    success: function (result) {
            //        $(".modal-content").html(result);
            //        $("#FuelcardsystemModalExtraLarge").modal("show");
            //    }
            //});
        } else if (response.RespStatus == 1) {
            Swal.fire("Topup details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemawardingformuladata() {
    var FormulaRules = new Array();
    $("#formularulestable TBODY TR").each(function () {
        var row = $(this);
        var FormulaRule = {};
        FormulaRule.Range1 = row.find("TD").eq(0).html();
        FormulaRule.Range2 = row.find("TD").eq(1).html();
        FormulaRule.IsRangetoInfinity = row.find("TD").eq(2).html();
        FormulaRule.Formula = row.find("TD").eq(4).html();
        FormulaRule.CreatedbyId = $('#systemLoggedinedUserid').val();
        FormulaRule.Createdby = $('#systemLoggedinedUserNameId').val();
        FormulaRule.ModifiedId = $('#systemLoggedinedUserid').val();
        FormulaRule.Modifiedby = $('#systemLoggedinedUserNameId').val();
        FormulaRule.Datecreated = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ');
        FormulaRule.Datemodified = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ');
        FormulaRules.push(FormulaRule);
    });
    var uil = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        FormulaId: $('#FormulaId').val(),
        FormulaName: $('#FormulanameId').val(),
        ValueType: $('#FormulaAwardTypeId').val(),
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
        Formularules: FormulaRules
    };
    $.post("/LoyaltyManagement/Addstemawardingformuladata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Formula details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Formula details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemformuladetaildata() {
    var uil = {
        FormulaId: $('#SystemFormulaId').val(),
        FormulaName: $('#SystemFormulaNameId').val(),
        ValueType: $('#FormulaAwardTypeId').val(),
        ModifiedBy: $('#systemLoggedinedUserid').val(),
        DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/LoyaltyManagement/Editsystemformuladata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Formula details Not Updated", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savesystemformularuleeditdetaildata() {
    var uil = {
        FormulaRuleId: $('#SystemFormulaRuleId').val(),
        Range1: $('#FormularulestartvalueId').val(),
        Range2: $('#FormularuleendvalueId').val(),
        IsRangetoInfinity: $('#FormularuleinfinityId').is(':checked'),
        Formula: $('#FormularuleformulaId').val(),
        ModifiedBy: $('#systemLoggedinedUserid').val(),
        DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/LoyaltyManagement/Editsystemformularuledata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Formula Rule details Not Updated", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savesystemawardingschemedata() {
    var SchemeRules = new Array();
    $("#schemerulestable TBODY TR").each(function () {
        var row = $(this);
        var SchemeRule = {};
        SchemeRule.DaysApplicable = row.find("TD").eq(0).html().split(',');
        SchemeRule.StartTime = row.find("TD").eq(1).html();
        SchemeRule.EndTime = row.find("TD").eq(2).html();
        SchemeRule.StationId = row.find("TD").eq(3).attr('data-id').split(',');
        SchemeRule.ProductId = row.find("TD").eq(4).attr('data-id').split(',');
        SchemeRule.LoyaltygroupId = row.find("TD").eq(5).attr('data-id').split(',');
        SchemeRule.PaymentmodeId = row.find("TD").eq(6).attr('data-id').split(',');
        SchemeRule.FormulaId = row.find("TD").eq(7).attr('data-id');
        SchemeRule.LRewardId = row.find("TD").eq(8).attr('data-id');
        SchemeRule.CreatedbyId = $('#systemLoggedinedUserid').val();
        SchemeRule.Createdby = $('#systemLoggedinedUserNameId').val();
        SchemeRule.ModifiedId = $('#systemLoggedinedUserid').val();
        SchemeRule.Modifiedby = $('#systemLoggedinedUserNameId').val();
        SchemeRule.Datecreated = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ');
        SchemeRule.Datemodified = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ');
        SchemeRules.push(SchemeRule);

    });
    var uil = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        LSchemeId: $('#SchemeId').val(),
        LSchemeName: $('#SchemenameId').val(),
        StartDate: $('#SchemestartdateId').val(),
        EndDate: $('#SchemeenddateId').val(),
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
        Schemerules: SchemeRules
    };
    $.post("/LoyaltyManagement/Addstemawardingmschemedata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Scheme details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Scheme details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemschemedetaildata() {
    var uil = {
        LSchemeId: $('#SystemLSchemeId').val(),
        LSchemeName: $('#SystemLSchemeNameId').val(),
        StartDate: $('#SystemSchemeStartDateId').val(),
        EndDate: $('#SystemSchemeEndDateId').val(),
        ModifiedBy: $('#systemLoggedinedUserid').val(),
        DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/LoyaltyManagement/Editsystemschemedata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Scheme details Not Updated", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemawardingschemeeditdata() {
    var uil = {
        LSchemeRuleId: $('#LSchemeRuleIdId').val(),
        FormulaId: $('#SchemeRuleFormulaId').val(),
        LRewardId: $('#SchemeRuleRewardId').val(),
        DaysApplicable: $('#SchemeRuleDaysApplicableId').val().join(','),
        StartTime: $('#SchemeRuleStartDateId').val(),
        EndTime: $('#SchemeRuleEndDateId').val(),
        ProductId: $('#SchemeRuleProductsId').val(),
        StationId: $('#SchemeRuleStationsId').val(),
        LoyaltygroupId: $('#SchemeRuleLoyaltyGroupsId').val(),
        PaymentmodeId: $('#SchemeRulePaymentModesId').val(),
        ModifiedId: $('#systemLoggedinedUserid').val(),
        Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/LoyaltyManagement/Editsystemschemeruledata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Scheme Rules details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemstationdata() {
    document.getElementById("saveSystemStationBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";

    if ($('#SystemstationnameId').val() == "") {
        Swal.fire("Station details Not saved", 'Stationname is Required', "warning");
        document.getElementById("saveSystemStationBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        return;
    }
    if ($('#SystemstationemailId').val() == "") {
        Swal.fire("Station details Not saved", 'Station Email is Required', "warning");
        document.getElementById("saveSystemStationBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        return;
    }
    if ($('#inputGroup-sizing-sm').val() == '' || $('#inputGroup-sizing-sm').val() == 0) {
        Swal.fire("Staff Detail Not Created", 'Phonenumber Prefix is Required', "warning");
        document.getElementById("saveSystemStationBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#SystemstationphoneId').val() == '') {
        Swal.fire("Staff Detail Not Created", 'Phonenumber is Required', "warning");
        document.getElementById("saveSystemStationBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }

    var uil = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StationId: $('#SystemstationId').val(),
        Sname: $('#SystemstationnameId').val(),
        Semail: $('#SystemstationemailId').val(),
        Phoneid: $('#inputGroup-sizing-sm').val(),
        Phone: $('#SystemstationphoneId').val(),
        Addresses: $('#SystemstationaddressId').val(),
        City: $('#SystemstationaddressId').val(),
        Street: $('#SystemstationaddressId').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/StationManagement/Addsystemstationdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Station details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Station details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
    document.getElementById("saveSystemStationBtn").disabled = false;
    document.getElementById("processingSpinner").style.display = "none";
    document.getElementById("buttonText").innerText = "SAVE";
}

function automatesystemstationdata(event) {
    var automateBtn = event.target;

    document.getElementsByClassName("switchtoautobtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    var uil1 = {
        TenantId: $('#systemLoggedinedTenantid').val(), StationId: event.currentTarget.getAttribute("data-Entryid"), Automatedby: $('#systemLoggedinedUserid').val(), Isautomated: event.currentTarget.getAttribute("data-Status"), DateAutomated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/StationManagement/Automatesystemstationdata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success');
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Automation failed", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementsByClassName("switchtoautobtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "AUTOMATE";
    });
}
function switchtomanualsystemstationdata(event) {
    document.getElementsByClassName("switchtomanualbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    var uil1 = {
        TenantId: $('#systemLoggedinedTenantid').val(), StationId: event.currentTarget.getAttribute("data-Entryid"), Automatedby: $('#systemLoggedinedUserid').val(), Isautomated: event.currentTarget.getAttribute("data-Status"), DateAutomated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    $.post("/StationManagement/Automatesystemstationdata", uil1, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success');
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Automation failed", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementsByClassName("switchtomanualbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SWITCH TO MANUAL";
    });
}
function savesystemstationtankdata() {
    var uil =
    {
        TankId: $('#SystemstationtankId').val(), StationId: $('#SystemstationtankstationId').val(), Name: $('#SystemstationtanknameId').val(),
        ProductVariationId: $('#SystemstationtankproductId').val(), Volume: $('#SystemstationtankvolumeId').val(), Diameter: $('#SystemstationtankdiamenterId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), TenantId: $('#systemLoggedinedTenantid').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/StationManagement/AddSystemStationTankdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Station Tank Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemstationpumpdata() {
    document.getElementsByClassName("savesystemstationpumpBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    var StationPumpNozzlesdata = [];
    $("#stationpumpnozzletable TBODY TR").each(function () {
        var row = $(this);
        var isDoubleSided = $("#StationIsDoubleSidedId").prop("checked");
        if (isDoubleSided) {
            // Loop for SideA
            var StationPumpNozzleA = {};
            StationPumpNozzleA.Nozzle = row.find("td").eq(0).text() + "-SideA"; // Assuming Nozzle data is at index 0
            StationPumpNozzleA.Side = "SideA"; // Set SideA explicitly
            StationPumpNozzleA.TankId = row.find("td").eq(2).text(); // Assuming TankId data is at index 2
            StationPumpNozzlesdata.push(StationPumpNozzleA);
            // Loop for SideB
            var StationPumpNozzleB = {};
            StationPumpNozzleB.Nozzle = row.find("td").eq(0).text() + "-SideB"; // Assuming Nozzle data is at index 0
            StationPumpNozzleB.Side = "SideB"; // Set SideB explicitly
            StationPumpNozzleB.TankId = row.find("td").eq(4).text(); // Assuming TankId data for SideB is at index 4
            StationPumpNozzlesdata.push(StationPumpNozzleB);
        } else {
            // Loop for single-sided
            var StationPumpNozzle = {};
            StationPumpNozzle.Nozzle = row.find("td").eq(0).text()+"-"+row.find("td").eq(1).text(); // Assuming Nozzle data is at index 0
            StationPumpNozzle.Side = row.find("td").eq(1).text(); // Assuming Side data is at index 1
            StationPumpNozzle.TankId = row.find("td").eq(2).text(); // Assuming TankId data is at index 2
            StationPumpNozzlesdata.push(StationPumpNozzle);
        }
    });
    var uil = {
        Stationid: $('#StationId').val(),
        Pumpid: $('#StationPumpId').val(),
        Tankid: $('#SystemstationtankId').val(),
        Pumpname: $('#StationPumpnameId').val(),
        Pumpmodel: $('#StationPumpmodelId').val(),
        Pumpnozzle: $('#StationnumberofnozzleId').val(),
        IsDoubleSided: $('#StationIsDoubleSidedId').is(':checked'),
        CreatedBy: $('#systemLoggedinedUserid').val(), ModifiedBy: $('#systemLoggedinedUserid').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
        StationPumpNozzles: StationPumpNozzlesdata
    };
    $.post("/StationManagement/Addsystemstationpumpdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Station Pump details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Station Pump details  Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementsByClassName("savesystemstationpumpBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}

function savesystemstationtankdipsdata() {
    $('#progressIndicator').show();
    document.getElementById("savesystemstationtankdipsBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#SystemstationtankId').val() == '' || $('#SystemstationtankId').val() == 0) {
        Swal.fire("Station Tank Dip Not Created", 'Tank is Required', "warning");
        document.getElementById("savesystemstationtankdipsBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }

    if ($('#StationCurrentDipReadingId').val() == '' || $('#StationCurrentDipReadingId').val() == 0) {
        Swal.fire("Station Tank Dip Not Created", 'Current Dip is Required', "warning");
        document.getElementById("savesystemstationtankdipsBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }

    if ($('#StationCurrentMeterReadingId').val() == '' || $('#StationCurrentMeterReadingId').val() == 0) {
        Swal.fire("Station Tank Dip Not Created", 'Current Meter is Required', "warning");
        document.getElementById("savesystemstationtankdipsBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    
    var uil =
    {
        StationDipId: $('#StationDipId').val(),StationId: $('#StationId').val(),TankId: $('#SystemstationtankId').val(),CurrentDipReading: $('#StationCurrentDipReadingId').val(),CurrentMeterReading: $('#StationCurrentMeterReadingId').val(),PreviousDipReading: $('#StationPreviousDipReadingId').val(),
        PreviousMeterReading: $('#StationPreviousMeterReadingId').val(), TankVariance: $('#StationTankVarianceId').val(), MeterVariance: $('#StationMeterVarianceId').val(), TankSale: $('#StationTankSaleId').val(), MeterSale: $('#StationMeterSaleId').val(),
        Extra: $('#ExtraId').val(), Extra1: $('#Extra1Id').val(), Extra2: $('#Extra2Id').val(), Extra3: $('#Extra3Id').val(), Extra4: $('#Extra4Id').val(),
        Extra5: $('#Extra5Id').val(), Extra6: $('#Extra6Id').val(), Extra7: $('#Extra7Id').val(), Extra8: $('#Extra8Id').val(), Extra9: $('#Extra9Id').val(), Extra10: $('#Extra10Id').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/StationManagement/Addsystemstationtankdipsdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            $('#progressIndicator').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Station Tank Dip Not Created", response.RespMessage, "warning");
            $('#progressIndicator').hide();
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
            $('#progressIndicator').hide();
        }
    });
    document.getElementById("savesystemstationtankdipsBtn").disabled = false;
    document.getElementById("processingSpinner").style.display = "none";
    document.getElementById("buttonText").innerText = "SAVE";
}

function savesystemstationpurchasesdata() {
    $('#progressIndicator').show();
    document.getElementById("savesystemstationpurchasesBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#SystemInvoiceNumberId').val() == '') {
        Swal.fire("Station Purchase Not Created", 'Invoice Number Required', "warning");
        document.getElementById("savesystemstationpurchasesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }

    if ($('#SystemInvoiceAmountId').val() == '') {
        Swal.fire("Station Purchase Not Created", 'Invoice Amount Required', "warning");
        document.getElementById("savesystemstationpurchasesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#SystemsupplierId').val() == '' || $('#SystemsupplierId').val() == 0) {
        Swal.fire("Station Purchase Not Created", 'Supplier is Required', "warning");
        document.getElementById("savesystemstationpurchasesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#SystemInvoiceDateId').val() == '') {
        Swal.fire("Station Purchase Not Created", 'Invoice Date is Required', "warning");
        document.getElementById("savesystemstationpurchasesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }

    var StationPurchaseItems = new Array();
    $("#stationpurchasestable TBODY TR").each(function () {
        var row = $(this);
        var StationPurchaseItem = {};
        StationPurchaseItem.ProductVariationId = row.find("TD").eq(0).html();
        StationPurchaseItem.PurchaseQuantity = row.find("TD").eq(2).html();
        StationPurchaseItem.PurchasePrice = row.find("TD").eq(3).html();
        StationPurchaseItem.PurchaseDiscount = 0.00;
        StationPurchaseItem.PurchaseTotal = row.find("TD").eq(4).html();
        StationPurchaseItem.Createdby = $('#systemLoggedinedUserid').val();
        StationPurchaseItem.Modifiedby = $('#systemLoggedinedUserid').val();
        StationPurchaseItem.Datecreated = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ');
        StationPurchaseItem.Datemodified = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ');
        StationPurchaseItems.push(StationPurchaseItem);
    });

    var uil =
    {
        PurchaseId: $('#PurchaseId').val(),StationId: $('#StationId').val(),InvoiceNumber: $('#SystemInvoiceNumberId').val(),
        SupplierId: $('#SystemsupplierId').val(), InvoiceAmount: $('#SystemInvoiceAmountId').val(), InvoiceDate: $('#SystemInvoiceDateId').val(),
        TruckNumber: $('#SystemInvoiceTruckNumberId').val(), DriverName: $('#SystemInvoiceDriverNameId').val(),
        Extra: $('#ExtraId').val(), Extra1: $('#Extra1Id').val(), Extra2: $('#Extra2Id').val(), Extra3: $('#Extra3Id').val(), Extra4: $('#Extra4Id').val(),
        Extra5: $('#Extra5Id').val(), Extra6: $('#Extra6Id').val(), Extra7: $('#Extra7Id').val(), Extra8: $('#Extra8Id').val(), Extra9: $('#Extra9Id').val(), Extra10: $('#Extra10Id').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
        StationPurchaseItem: StationPurchaseItems
    };
    $.post("/StationManagement/Addsystemstationpurchasesdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            $('#progressIndicator').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Station Purchases Not Created", response.RespMessage, "warning");
            $('#progressIndicator').hide();
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
            $('#progressIndicator').hide();
        }
    });
    document.getElementById("savesystemstationpurchasesBtn").disabled = false;
    document.getElementById("processingSpinner").style.display = "none";
    document.getElementById("buttonText").innerText = "SAVE";
}

function savesystemstationshiftdetaildata() {
    document.getElementById("saveSystemStationShiftBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";

    if ($('#ShiftcategoryId').val() == '') {
        Swal.fire("Shift Not Created", 'Shift Type is Required', "warning");
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
        return;
    }

    var ShiftPumpReadingsData = [];
    var ShiftTankReadingsData = [];
    var ShiftLubeReadingsData = [];
    var ShiftLpgsReadingsData = [];
    var ShiftSparePartsData = [];
    var ShiftCarWashsData = [];

    var isPumpValid = true; // Initialize isValid outside the loop
    var isTankValid = true; // Initialize isValid outside the loop
    var isLubeValid = true; // Initialize isValid outside the loop
    var isLpgValid = true; // Initialize isValid outside the loop
    var isSpareValid = true; // Initialize isValid outside the loop
    var isCarWashValid = true; // Initialize isValid outside the loop



    $('#AllsystemDataTables').DataTable().$('tr', { "filter": "applied" }).each(function () {
        var row = $(this);

        // Retrieve values from input fields within the row
        var StationShiftId = $("#SystemstationShiftId").val();
        var pumpCashierAttendant = row.find('.pump-cashier-attendant').val();
        var pumpOpeningShilling = row.find('.pump-opening-shilling').val();
        var pumpOpeningElectronic = row.find('.pump-opening-electronic').val();
        var pumpOpeningManual = row.find('.pump-opening-manual').val();
        var pumpClosingShilling = row.find('.pump-closing-shilling').val();
        var pumpClosingElectronic = row.find('.pump-closing-electronic').val();
        var pumpClosingManual = row.find('.pump-closing-manual').val();
        var TestingRtt = row.find('.pump-testing-rtt').val();
        var ElectronicSold = row.find('.pump-electronic-sold').val();
        var TotalShilling = row.find('.pump-total-shilling').val();
        var ElectronicAmount = row.find('.pump-electronic-amount').val();
        var ShillingDifference = row.find('.pump-shilling-difference').val();
        var LitersDifference = row.find('.pump-liters-difference').val();
        var ShiftReference = row.find('.pump-shift-reference').val();
        var ManualSold = row.find('.pump-manual-manualsold').val();
        var ManualAmount = row.find('.pump-manual-amount').val();
        var PumpRttAmount = row.find('.pump-rtt-amount').val();
        var TotalPumpAmount = row.find('.pump-total-amount').val();

        // Remove any previous validation classes
        row.find('.is-invalid').removeClass('is-invalid');
        row.find('.is-valid').removeClass('is-valid');

        var rowIsValid = true; // Initialize rowIsValid for each row

        // Perform validation checks
        if (StationShiftId == 0) {
            if (pumpCashierAttendant == "" || pumpCashierAttendant == 0) {
                row.find('.pump-cashier-attendant').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.pump-cashier-attendant').addClass('is-valid');
            }

            if (pumpOpeningShilling == "" || pumpOpeningShilling == 0) {
                row.find('.pump-opening-shilling').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.pump-opening-shilling').addClass('is-valid');
            }

            if (pumpOpeningElectronic == "" || pumpOpeningElectronic == 0) {
                row.find('.pump-opening-electronic').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.pump-opening-electronic').addClass('is-valid');
            }

            if (pumpOpeningManual == "" || pumpOpeningManual == 0) {
                row.find('.pump-opening-manual').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.pump-opening-manual').addClass('is-valid');
            }
        } else {
            if (pumpClosingShilling == "" || pumpClosingShilling == 0 || parseFloat(pumpClosingShilling) < parseFloat(pumpOpeningShilling)) {
                row.find('.pump-closing-shilling').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.pump-closing-shilling').addClass('is-valid');
            }

            if (pumpClosingElectronic == "" || pumpClosingElectronic == 0 || parseFloat(pumpClosingElectronic) < parseFloat(pumpOpeningElectronic)) {
                row.find('.pump-closing-electronic').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.pump-closing-electronic').addClass('is-valid');
            }

            if (pumpClosingManual == "" || pumpClosingManual == 0 || parseFloat(pumpClosingManual) < parseFloat(pumpOpeningManual)) {
                row.find('.pump-closing-manual').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.pump-closing-manual').addClass('is-valid');
            }
        }

        if (!rowIsValid) {
            isPumpValid = false; // If any row is invalid, set isPumpValid to false
        } else {
            // Only push data if the row is valid
            var ShiftPumpReadingData = {
                ShiftPumpReadingId: row.find('td:eq(0)').text(),
                ShiftId: row.find('td:eq(1)').text(), 
                NozzleId: row.find('td:eq(2)').text(),
                PumpId: row.find('td:eq(3)').text(),
                AttendantId: pumpCashierAttendant,
                ProductPrice: row.find('.pump-product-price').val().replace(/,/g, ''),
                OpeningShilling: pumpOpeningShilling.replace(/,/g, ''),
                OpeningElectronic: pumpOpeningElectronic.replace(/,/g, ''),
                OpeningManual: pumpOpeningManual.replace(/,/g, ''),
                ClosingShilling: pumpClosingShilling.replace(/,/g, ''),
                ClosingElectronic: pumpClosingElectronic.replace(/,/g, ''),
                ClosingManual: pumpClosingManual.replace(/,/g, ''),
                TestingRtt: TestingRtt.replace(/,/g, ''),
                ElectronicSold: ElectronicSold.replace(/,/g, ''),
                TotalShilling: TotalShilling.replace(/,/g, ''),
                ElectronicAmount: ElectronicAmount.replace(/,/g, ''),
                ShillingDifference: ShillingDifference.replace(/,/g, ''),
                LitersDifference: LitersDifference.replace(/,/g, ''),
                ShiftReference: ShiftReference,
                ManualSold: ManualSold.replace(/,/g, ''),
                ManualAmount: ManualAmount.replace(/,/g, ''),
                PumpRttAmount: PumpRttAmount.replace(/,/g, ''),
                TotalPumpAmount: TotalPumpAmount.replace(/,/g, ''),
                Createdby: $('#systemLoggedinedUserid').val(),
                Modifiedby: $('#systemLoggedinedUserid').val(),
                DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
                DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
            };
            ShiftPumpReadingsData.push(ShiftPumpReadingData);
        }
    });
    // Check isPumpValid flag
    if (!isPumpValid) {
        Swal.fire("Shift Not Created", 'Pump reading is required', "warning");
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
        return;
    }
    $("#AllsystemDataTables1").DataTable().$('tr', { "filter": "applied" }).each(function () {
        var row = $(this);
        var StationShiftId = $("#SystemstationShiftId").val();
        var tankCashierAttendant = row.find('.tank-cashier-attendant').val();
        var tankOpeningQuantity = row.find('.tank-opening-quantity').val();
        var tankClosingQuantity = row.find('.tank-closing-quantity').val();

        // Remove any previous validation classes
        row.find('.is-invalid').removeClass('is-invalid');
        row.find('.is-valid').removeClass('is-valid');

        var rowIsValid = true;
        if (StationShiftId == 0) {
            if (tankCashierAttendant == "" || tankCashierAttendant == 0) {
                row.find('.tank-cashier-attendant').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.tank-cashier-attendant').addClass('is-valid');
            }

            if (tankOpeningQuantity == "" || tankOpeningQuantity == 0) {
                row.find('.tank-opening-quantity').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.tank-opening-quantity').addClass('is-valid');
            }
        } else {
            if (tankClosingQuantity == "" || tankClosingQuantity == 0) {
                row.find('.tank-closing-quantity').addClass('is-invalid');
                rowIsValid = false;
            } else {
                row.find('.tank-closing-quantity').addClass('is-valid');
            }
        }
        if (!rowIsValid) {
            isTankValid = false; // If any row is invalid, set isValid to false
        }
        if (isTankValid) {
            var ShiftTankReadingData = {
                ShiftTankReadingId: row.find('td:eq(0)').text(),
                ShiftId: row.find('td:eq(1)').text(), 
                TankId: row.find('td:eq(2)').text(),
                AttendantId: row.find('.tank-cashier-attendant').val(),
                ProductPrice: row.find('.tank-product-price').val().replace(/,/g, ''),
                OpeningQuantity: row.find('.tank-opening-quantity').val().replace(/,/g, ''),
                ClosingQuantity: row.find('.tank-closing-quantity').val().replace(/,/g, ''),
                QuantitySold: row.find('.tank-quantity-sold').val().replace(/,/g, ''),
                AmountSold: row.find('.tank-amount-sold').val().replace(/,/g, ''),
                Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
                DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
                DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            ShiftTankReadingsData.push(ShiftTankReadingData);
        }
    });
    // Check isTankValid flag
    if (!isTankValid) {
        Swal.fire("Shift Not Created", 'Tank reading is required', "warning");
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
        return;
    }



    $("#AllsystemDataTables2").DataTable().$('tr', { "filter": "applied" }).each(function () {
        var row = $(this);
        var StationShiftId = $("#SystemstationShiftId").val();
        var lubesCashierAttendant = row.find('.lubes-cashier-attendant').val();
        var lubesOpeningUnits = row.find('.lubes-opening-units').val();
        var lubesClosingUnits = row.find('.lubes-closing-units').val();

        // Remove any previous validation classes
        row.find('.is-invalid').removeClass('is-invalid');
        row.find('.is-valid').removeClass('is-valid');

        var rowIsValid = true;
        if ($("#AllsystemDataTables2 tbody tr").length > 0) {
            $("#AllsystemDataTables2 tbody tr").each(function () {
                if (StationShiftId == 0) {
                    if (lubesCashierAttendant == "" || lubesCashierAttendant == 0) {
                        row.find('.lubes-cashier-attendant').addClass('is-invalid');
                        rowIsValid = false;
                    } else {
                        row.find('.lubes-cashier-attendant').addClass('is-valid');
                    }

                    if (lubesOpeningUnits == "") {
                        row.find('.lubes-opening-units').addClass('is-invalid');
                        rowIsValid = false;
                    } else {
                        row.find('.lubes-opening-units').addClass('is-valid');
                    }
                } else {

                    if (lubesClosingUnits == "") {
                        row.find('.lubes-closing-units').addClass('is-invalid');
                        rowIsValid = false;
                    } else {
                        row.find('.lubes-closing-units').addClass('is-valid');
                    }
                }

            });
        }
        if (!rowIsValid) {
            isLubeValid = false; // If any row is invalid, set isValid to false
        }

        if (isLubeValid) {
            var ShiftLubeReadingData = {
                ShiftLubeLpgId: row.find('td:eq(0)').text(),
                ShiftId: row.find('td:eq(1)').text(), 
                ProductVariationId: row.find('td:eq(2)').text(),
                StockTypeId: 0,
                AttendantId: row.find('.lubes-cashier-attendant').val(),
                OpeningUnits: row.find('.lubes-opening-units').val().replace(/,/g, ''),
                ClosingUnits: row.find('.lubes-closing-units').val().replace(/,/g, ''),
                UnitsSold: row.find('.lubes-units-sold').val().replace(/,/g, ''),
                ProductPrice: row.find('.lubes-unit-price').val().replace(/,/g, ''),
                UnitsTotal: row.find('.lubes-amount-sold').val().replace(/,/g, ''),
                Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
                DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
                DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            ShiftLubeReadingsData.push(ShiftLubeReadingData);
        }
    });
    // Check isLubeValid flag
    if (!isLubeValid) {
        Swal.fire("Shift Not Created", 'Lubelicants reading is required', "warning");
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
        return;
    }

    $("#AllsystemDataTables3").DataTable().$('tr', { "filter": "applied" }).each(function () {
        var row = $(this);
        var StationShiftId = $("#SystemstationShiftId").val();
        var lpgCashierAttendant = row.find('.lpg-cashier-attendant').val();
        var lpgOpeningUnits = row.find('.lpg-opening-units').val();
        var lpgClosingUnits = row.find('.lpg-closing-units').val();

        // Remove any previous validation classes
        row.find('.is-invalid').removeClass('is-invalid');
        row.find('.is-valid').removeClass('is-valid');

        var rowIsValid = true;
        if ($("#AllsystemDataTables3 tbody tr").length > 0) {
            if (StationShiftId == 0) {
                if (lpgCashierAttendant == "" || lpgCashierAttendant == 0) {
                    row.find('.lpg-cashier-attendant').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.lpg-cashier-attendant').addClass('is-valid');
                }

                if (lpgOpeningUnits == "") {
                    row.find('.lpg-opening-units').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.lpg-opening-units').addClass('is-valid');
                }
            } else {
                if (lpgClosingUnits == "") {
                    row.find('.lpg-closing-units').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.lpg-closing-units').addClass('is-valid');
                }
            }
        }
        if (!rowIsValid) {
            isLpgValid = false; // If any row is invalid, set isValid to false
        }

        if (isLpgValid) {
            var ShiftLpgReadingsData = {
                ShiftLubeLpgId: row.find('td:eq(0)').text(),
                ShiftId: row.find('td:eq(1)').text(), 
                ProductVariationId: row.find('td:eq(2)').text(),
                StockTypeId: 0,
                AttendantId: row.find('.lpg-cashier-attendant').val(),
                OpeningUnits: row.find('.lpg-opening-units').val().replace(/,/g, ''),
                ClosingUnits: row.find('.lpg-closing-units').val().replace(/,/g, ''),
                UnitsSold: row.find('.lpg-units-sold').val().replace(/,/g, ''),
                ProductPrice: row.find('.lpg-unit-price').val().replace(/,/g, ''),
                UnitsTotal: row.find('.lpg-amount-sold').val().replace(/,/g, ''),
                Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
                DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
                DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            ShiftLpgsReadingsData.push(ShiftLpgReadingsData);
        }
    });
    // Check isLpgValid flag
    if (!isLpgValid) {
        Swal.fire("Shift Not Created", 'Lpg and accessories reading is required', "warning");
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
        return;
    }
    $("#AllsystemDataTables4").DataTable().$('tr', { "filter": "applied" }).each(function () {
        var row = $(this);
        var StationShiftId = $("#SystemstationShiftId").val();
        var sparePartsCashierAttendant = row.find('.spare-cashier-attendant').val();
        var sparePartsOpeningUnits = row.find('.spare-opening-units').val();
        var sparePartsClosingUnits = row.find('.spare-closing-units').val();

        // Remove any previous validation classes
        row.find('.is-invalid').removeClass('is-invalid');
        row.find('.is-valid').removeClass('is-valid');

        var rowIsValid = true;
        if ($("#AllsystemDataTables4 tbody tr").length > 0) {
            if (StationShiftId == 0) {
                if (sparePartsCashierAttendant == "" || sparePartsCashierAttendant == 0) {
                    row.find('.spare-cashier-attendant').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.spare-cashier-attendant').addClass('is-valid');
                }

                if (sparePartsOpeningUnits == "") {
                    row.find('.spare-opening-units').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.spare-opening-units').addClass('is-valid');
                }
            } else {
                if (sparePartsClosingUnits == "") {
                    row.find('.spare-closing-units').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.spare-closing-units').addClass('is-valid');
                }
            }
        }

        if (!rowIsValid) {
            isSpareValid = false; // If any row is invalid, set isValid to false
        }
        if (isSpareValid) {
            var ShiftSparePartData = {
                ShiftLubeLpgId: row.find('td:eq(0)').text(),
                ShiftId: row.find('td:eq(1)').text(),
                ProductVariationId: row.find('td:eq(2)').text(),
                StockTypeId: 0,
                AttendantId: row.find('.spare-cashier-attendant').val(),
                OpeningUnits: row.find('.spare-opening-units').val().replace(/,/g, ''),
                ClosingUnits: row.find('.spare-closing-units').val().replace(/,/g, ''),
                UnitsSold: row.find('.spare-units-sold').val().replace(/,/g, ''),
                ProductPrice: row.find('.spare-unit-price').val().replace(/,/g, ''),
                UnitsTotal: row.find('.spare-amount-sold').val().replace(/,/g, ''),
                Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
                DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
                DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            ShiftSparePartsData.push(ShiftSparePartData);
        }
    });
    // Check isSpareValid flag
    if (!isSpareValid) {
        Swal.fire("Shift Not Created", 'Spareparts reading is required', "warning");
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
        return;
    }
    $("#AllsystemDataTables5").DataTable().$('tr', { "filter": "applied" }).each(function () {
        var row = $(this);
        var StationShiftId = $("#SystemstationShiftId").val();
        var carWashCashierAttendant = row.find('.carwash-cashier-attendant').val();
        var carWashOpeningUnits = row.find('.carwash-opening-units').val();
        var carWashClosingUnits = row.find('.carwash-closing-units').val();

        // Remove any previous validation classes
        row.find('.is-invalid').removeClass('is-invalid');
        row.find('.is-valid').removeClass('is-valid');

        var rowIsValid = true;
        if ($("#AllsystemDataTables5 tbody tr").length > 0) {
            if (StationShiftId == 0) {
                if (carWashCashierAttendant == "" || carWashCashierAttendant == 0) {
                    row.find('.carwash-cashier-attendant').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.carwash-cashier-attendant').addClass('is-valid');
                }

                if (carWashOpeningUnits == "") {
                    row.find('.carwash-opening-units').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.carwash-opening-units').addClass('is-valid');
                }
            } else {
                if (carWashClosingUnits == "") {
                    row.find('.carwash-closing-units').addClass('is-invalid');
                    rowIsValid = false;
                } else {
                    row.find('.carwash-closing-units').addClass('is-valid');
                }
            }
        }

        if (!rowIsValid) {
            isCarWashValid = false; // If any row is invalid, set isValid to false
        }
        if (isCarWashValid) {
            var ShiftCarWashData = {
                ShiftLubeLpgId: row.find('td:eq(0)').text(),
                ShiftId: row.find('td:eq(1)').text(),
                ProductVariationId: row.find('td:eq(2)').text(),
                StockTypeId: 0,
                AttendantId: row.find('.carwash-cashier-attendant').val(),
                OpeningUnits: row.find('.carwash-opening-units').val().replace(/,/g, ''),
                ClosingUnits: row.find('.carwash-closing-units').val().replace(/,/g, ''),
                UnitsSold: row.find('.carwash-units-sold').val().replace(/,/g, ''),
                ProductPrice: row.find('.carwash-unit-price').val().replace(/,/g, ''),
                UnitsTotal: row.find('.carwash-amount-sold').val().replace(/,/g, ''),
                Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
                DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
                DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            ShiftCarWashsData.push(ShiftCarWashData);
        }
    });

    // Check isSpareValid flag
    if (!isCarWashValid) {
        Swal.fire("Shift Not Created", 'Car Wash reading is required', "warning");
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
        return;
    }
    var uil = {
        ShiftId: $("#SystemstationShiftId").val(), StationId: $("#SystemstationId").val(), ShiftCode: $("#SystemstationshiftcodeId").val(), ShiftCategory: $("#ShiftcategoryId").val(), CashOrAccount: $("#StationcashoraccountId").val(),
        ShiftDateTime: $("#SystemstationshiftdatetimeId").val(), ShiftStatus: $("#SystemStationShiftId").val(), IsPosted: $("#SystemStationShiftispostedId").val(), IsDeleted: $("#SystemStationShiftIsDeletedId").val(),
        ShiftTotalAmount: $("#StationshiftexpectedcashId").val(), ShiftBankedAmount: $("#StationshiftbankedcashId").val(), ShiftBalance: $("#StationshiftunbankedcashId").val(), ExpectedTankAmount: $("#StationshiftexpectedtankcashId").val(),
        ExpectedPumpAmount: $("#StationshiftexpectedpumpcashId").val(), GainLoss: $("#StationshiftgainlosscashId").val(), PercentGainLoss: $("#StationshiftpercentagegainlosscashId").val(), ShiftBankReference: $("#SystemStationShiftBankingReferenceId").val(), ShiftReference: $("#SystemStationShiftReferenceId").val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
        //ShiftPumpReading: ShiftPumpReadingsData, ShiftTankReading: ShiftTankReadingsData, ShiftLubeReading: ShiftLubeReadingsData,
        //ShiftLpgReading: ShiftLpgsReadingsData, ShiftSparePartsData: ShiftSparePartsData, ShiftCarWashsData: ShiftCarWashsData
    };
   
    $.post("/StationManagement/Addsystemstationshiftdata", uil, function (response) {
        if (response.RespStatus == 0) {
            alert(JSON.stringify(response));
            var shiftId = response.Data1;
            $("#SystemstationShiftId").val(shiftId);

            var shiftData = [
                { url: "/StationManagement/Addsystemstationshiftpumpdata", data: { ShiftId: shiftId, ShiftPumpReading: ShiftPumpReadingsData }, dataKey: "ShiftPumpReading" },
                { url: "/StationManagement/Addsystemstationshifttankdata", data: { ShiftId: shiftId, ShiftTankReading: ShiftTankReadingsData }, dataKey: "ShiftTankReading" },
                { url: "/StationManagement/Addsystemstationshiftlubedata", data: { ShiftId: shiftId, ShiftLubeReading: ShiftLubeReadingsData }, dataKey: "ShiftLubeReading" },
                { url: "/StationManagement/Addsystemstationshiftlpgdata", data: { ShiftId: shiftId, ShiftLpgReading: ShiftLpgsReadingsData }, dataKey: "ShiftLpgReading" },
                { url: "/StationManagement/Addsystemstationshiftsparepartdata", data: { ShiftId: shiftId, ShiftSparePartsData: ShiftSparePartsData }, dataKey: "ShiftSparePartsData" },
                { url: "/StationManagement/Addsystemstationshiftcarwashdata", data: { ShiftId: shiftId, ShiftCarWashsData: ShiftCarWashsData }, dataKey: "ShiftCarWashsData" }
            ];

            // Start the chain of requests
            postShiftData(0);

            function postShiftData(index) {
                if (index >= shiftData.length) {
                    Swal.fire("Success", "All shift details have been successfully saved.", "success");
                    window.location.href = "/StationManagement/Addsystemstationshift?ShiftId=" + $("#SystemstationShiftId").val();
                }

                var currentData = shiftData[index];
                var currentDataArray = currentData.data[currentData.dataKey];

                if (currentDataArray && currentDataArray.length > 0) {
                    $.post(currentData.url, currentData.data, function (response) {
                        if (response.RespStatus == 0) {
                            postShiftData(index + 1);
                        } else if (response.RespStatus == 1) {
                            Swal.fire("Shift Details Not saved", response.RespMessage, "warning");
                        } else {
                            Swal.fire("Oops! Something Went Wrong", "Database Error has occurred. Kindly contact our support team.", "error");
                        }
                    }).fail(function () {
                        Swal.fire("Oops! Something Went Wrong", "Network error. Please try again later.", "error");
                    });
                } else {
                    postShiftData(index + 1);
                }
            }
        } else if (response.RespStatus == 1) {
            Swal.fire("Station details Not saved", response.RespMessage, "warning");
        } else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occurred. Kindly contact our support team.", "error");
        }

        // Re-enable the button and hide the spinner
        document.getElementById("saveSystemStationShiftBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SUBMIT SHIFT DETAIL DATA";
    });
}

function closesystemstationshiftdetaildata() {
    document.getElementById("closeSystemStationShiftBtn").disabled = true;
    document.getElementById("closeProcessingSpinner").style.display = "inline-block";
    document.getElementById("closebuttonText").innerText = "Processing...";
    var uil = {
        ShiftId: $("#SystemstationShiftId").val(), StationId: $("#SystemstationId").val(), ShiftCode: $("#SystemstationshiftcodeId").val(), ShiftCategory: $("#ShiftcategoryId").val(), CashOrAccount: $("#StationcashoraccountId").val(),
        ShiftDateTime: $("#SystemstationshiftdatetimeId").val(), ShiftStatus: 1, IsPosted: $("#SystemStationShiftispostedId").val(), IsDeleted: $("#SystemStationShiftIsDeletedId").val(),
        ShiftTotalAmount: $("#StationshiftexpectedcashId").val(), ShiftBankedAmount: $("#StationshiftbankedcashId").val(), ShiftBalance: $("#StationshiftunbankedcashId").val(), ExpectedTankAmount: $("#StationshiftexpectedtankcashId").val(),
        ExpectedPumpAmount: $("#StationshiftexpectedpumpcashId").val(), GainLoss: $("#StationshiftgainlosscashId").val(), PercentGainLoss: $("#StationshiftpercentagegainlosscashId").val(), ShiftBankReference: $("#0").val(), ShiftReference: $("#SystemStationShiftReferenceId").val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };
    
    $.post("/StationManagement/Closesystemstationshiftdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Shift Closed!', response.RespMessage, 'success');
            setTimeout(function () { location.reload(); }, 1000);
            window.location.href = "/StationManagement/Shiftlistings";
        } else if (response.RespStatus == 1) {
            Swal.fire("Shift Not Closed", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
    document.getElementById("closeSystemStationShiftBtn").disabled = false;
    document.getElementById("closeProcessingSpinner").style.display = "none";
    document.getElementById("closebuttonText").innerText = "CLOSE SHIFT DETAIL DATA";
}

function supervisorclosesystemstationshiftdata() {
    document.getElementById("closeSystemStationShiftBtn").disabled = true;
    document.getElementById("closeProcessingSpinner").style.display = "inline-block";
    document.getElementById("closebuttonText").innerText = "Processing...";
    var uil = {
        ShiftId: $("#SystemstationShiftId").val(), StationId: $("#SystemstationId").val(), ShiftCode: $("#SystemstationshiftcodeId").val(), ShiftCategory: $("#ShiftcategoryId").val(), CashOrAccount: $("#StationcashoraccountId").val(),
        ShiftDateTime: $("#SystemstationshiftdatetimeId").val(), ShiftStatus: 1, IsPosted: $("#SystemStationShiftispostedId").val(), IsDeleted: $("#SystemStationShiftIsDeletedId").val(),
        ShiftTotalAmount: $("#StationshiftexpectedcashId").val(), ShiftBankedAmount: $("#StationshiftbankedcashId").val(), ShiftBalance: $("#StationshiftunbankedcashId").val(), ExpectedTankAmount: $("#StationshiftexpectedtankcashId").val(),
        ExpectedPumpAmount: $("#StationshiftexpectedpumpcashId").val(), GainLoss: $("#StationshiftgainlosscashId").val(), PercentGainLoss: $("#StationshiftpercentagegainlosscashId").val(), ShiftBankReference: $("#0").val(), ShiftReference: $("#SystemStationShiftReferenceId").val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ')
    };

    $.post("/StationManagement/Supervisorclosesystemstationshiftdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Shift Closed!', response.RespMessage, 'success');
            setTimeout(function () { location.reload(); }, 1000);
            window.location.href = "/StationManagement/Shiftlistings";
        } else if (response.RespStatus == 1) {
            Swal.fire("Shift Not Closed", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
    document.getElementById("closeSystemStationShiftBtn").disabled = false;
    document.getElementById("closeProcessingSpinner").style.display = "none";
    document.getElementById("closebuttonText").innerText = "CLOSE SHIFT DETAIL DATA";
}

function savesystemstationshiftvoucherdata() {
    document.getElementById("savesystemstationshiftvoucherBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#VoucherTypeId').val() == '') {
        Swal.fire("Shift Voucher Not Created", 'Shift Voucher Type is Required', "warning");
        document.getElementById("savesystemstationshiftvoucherBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#VoucherModeId').val() == '') {
        Swal.fire("Shift Voucher Not Created", 'Shift Voucher Mode is Required', "warning");
        document.getElementById("savesystemstationshiftvoucherBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#VoucherTypeId').val() == 'Payment') {
        if ($('#VoucherNameId').val() == '') {
            Swal.fire("Shift Voucher Not Created", 'Shift Voucher Name is Required', "warning");
            document.getElementById("savesystemstationshiftvoucherBtn").disabled = false;
            document.getElementById("processingSpinner").style.display = "none";
            document.getElementById("buttonText").innerText = "SAVE";
            return;
        }
    } 
    if ($('#VoucherAmountId').val() == '' || $('#VoucherAmountId').val() == 0) {
        Swal.fire("Shift Voucher Not Created", 'Shift Voucher Amount is Required', "warning");
        document.getElementById("savesystemstationshiftvoucherBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    var CreditDebit = '';
    var VoucherName = '';
    if ($('#VoucherTypeId').val() == 'Payment') {
        CreditDebit = 'Debit';
        VoucherName = $('#VoucherNameId').val()
    } else {
        CreditDebit = 'Credit';
        VoucherName = 'Fuel Sale Payment';
    }
    var uil =
    {
        ShiftVoucherId: $('#ShiftVoucherId').val(), ShiftId: $('#ShiftId').val(), AttendantId: $('#AttendantId').val(), VoucherType: $('#VoucherTypeId').val(), VoucherModeId: $('#VoucherModeId').val(), VoucherName: VoucherName, VoucherAmount: $('#VoucherAmountId').val(), CreditDebit: CreditDebit,
        Extra: $('#ExtraId').val(),Extra1: $('#Extra1Id').val(),Extra2: $('#Extra2Id').val(),Extra3: $('#Extra3Id').val(),Extra4: $('#Extra4Id').val(),
        Extra5: $('#Extra5Id').val(),Extra6: $('#Extra6Id').val(),Extra7: $('#Extra7Id').val(),Extra8: $('#Extra8Id').val(),Extra9: $('#Extra9Id').val(),Extra10: $('#Extra10Id').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/StationManagement/Addsystemstationshiftvoucherdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            window.location.href = "/StationManagement/ShiftDataDetails#StationCreditSales";
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Shift Voucher Not Created", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
    document.getElementById("savesystemstationshiftvoucherBtn").disabled = false;
    document.getElementById("processingSpinner").style.display = "none";
    document.getElementById("buttonText").innerText = "SAVE";
}

function savesystemstationshiftlubeandlpgdata() {
    $('#progressIndicator').show();
    document.getElementById("savesystemstationshiftlubeandlpgBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#ProductVariationId').val() == '') {
        Swal.fire("Shift Lube and Lpg Not Created", 'Product Item is Required', "warning");
        document.getElementById("savesystemstationshiftlubeandlpgBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#ItemQuantityId').val() == '') {
        Swal.fire("Shift Lube and Lpg Not Created", 'Product Quantity is Required', "warning");
        document.getElementById("savesystemstationshiftlubeandlpgBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    var uil =
    {
        ShiftLubeLpgId: $('#ShiftLubeLpgId').val(), ShiftId: $('#ShiftId').val(), AttendantId: $('#AttendantId').val(), ProductVariationId: $('#ProductVariationId').val(), ProductUnits: $('#ItemQuantityId').val(), ProductPrice: $('#ItemPriceId').val(), ProductTotal: $('#ItemtotaAmountId').val(),
        Extra: $('#ExtraId').val(), Extra1: $('#Extra1Id').val(), Extra2: $('#Extra2Id').val(), Extra3: $('#Extra3Id').val(), Extra4: $('#Extra4Id').val(),
        Extra5: $('#Extra5Id').val(), Extra6: $('#Extra6Id').val(), Extra7: $('#Extra7Id').val(), Extra8: $('#Extra8Id').val(), Extra9: $('#Extra9Id').val(), Extra10: $('#Extra10Id').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/StationManagement/Addsystemstationlubedata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            $('#progressIndicator').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Shift Lubes and Lpg Not Created", response.RespMessage, "warning");
            $('#progressIndicator').hide();
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
            $('#progressIndicator').hide();
        }
    });
    document.getElementById("savesystemstationshiftvoucherBtn").disabled = false;
    document.getElementById("processingSpinner").style.display = "none";
    document.getElementById("buttonText").innerText = "SAVE";
}

function savesystemstationshiftcreditinvoicedata() {
    $('#progressIndicator').show();
    document.getElementById("savesystemstationshiftcreditinvoiceBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    if ($('#SearchCustomerId').val() == '' || $('#SearchCustomerId').val() == 0) {
        Swal.fire("Shift Credit Invoice Not Created", 'Customer is Required', "warning");
        document.getElementById("savesystemstationshiftcreditinvoiceBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#ProductVariationId').val() == '' || $('#ProductVariationId').val() == 0) {
        Swal.fire("Shift Credit Invoice Not Created", 'Product is Required', "warning");
        document.getElementById("savesystemstationshiftcreditinvoiceBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#CustomerEquipmentId').val() == '' || $('#CustomerEquipmentId').val() == 0) {
        Swal.fire("Shift Credit Invoice Not Created", 'Equipment is Required', "warning");
        document.getElementById("savesystemstationshiftcreditinvoiceBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#ItemQuantityId').val() == '' || $('#ItemQuantityId').val() == 0) {
        Swal.fire("Shift Credit Invoice Not Created", 'Item Quantity is Required', "warning");
        document.getElementById("savesystemstationshiftcreditinvoiceBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    var uil =
    {
        ShiftCreditInvoiceId: $('#ShiftCreditInvoiceId').val(), ShiftId: $('#ShiftId').val(), AttendantId: $('#AttendantId').val(), CustomerId: $('#SearchCustomerId').val(), EquipmentId: $('#CustomerEquipmentId').val(), ProductVariationId: $('#ProductVariationId').val(), ProductUnits: $('#ItemQuantityId').val(), ProductPrice: $('#ItemPriceId').val(), ProductDiscount: $('#ItemDiscountId').val(), ProductTotal: $('#ItemtotaAmountId').val(),
        Extra: $('#ExtraId').val(), Extra1: $('#Extra1Id').val(), Extra2: $('#Extra2Id').val(), Extra3: $('#Extra3Id').val(), Extra4: $('#Extra4Id').val(),
        Extra5: $('#Extra5Id').val(), Extra6: $('#Extra6Id').val(), Extra7: $('#Extra7Id').val(), Extra8: $('#Extra8Id').val(), Extra9: $('#Extra9Id').val(), Extra10: $('#Extra10Id').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/StationManagement/Addsystemcreditinvoicesaledata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            $('#progressIndicator').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Shift Credit Invoice Not Created", response.RespMessage, "warning");
            $('#progressIndicator').hide();
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
            $('#progressIndicator').hide();
        }
    });
    document.getElementById("savesystemstationshiftcreditinvoiceBtn").disabled = false;
    document.getElementById("processingSpinner").style.display = "none";
    document.getElementById("buttonText").innerText = "SAVE";
}
function savesystemproductdata() {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
    var Productstations = new Array();
    checkboxes.forEach((checkbox) => {
        var Productstation = {};
        Productstation.StationId = checkbox.value;
        Productstations.push(Productstation);
    });
    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), ProductId: $('#ProductId').val(), ProductvariationId: $('#ProductvariationId').val(),
        Productname: $('#ProductNameId').val(), CategoryId: $('#ProductCategoryId').val(), UomId: $('#ProductBaseuomId').val(), Barcode: $('#ProductbarCodeId').val(), Productprice: $('#ProductPriceId').val(), ProductVat: $('#ProductVatvalueId').val(), ProductPriceStations: Productstations, CreatedbyId: $('#systemLoggedinedUserid').val(), ModifiedId: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/ProductManagement/Addsystemproductdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Product details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Product details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}
function updatesystemproductdata() {
    var uil =
    {
        ProductvariationId: $('#ProductvariationId').val(), ProductId: $('#ProductId').val(),
        Productvariationname: $('#ProductNameId').val(), CategoryId: $('#ProductCategoryId').val(), UomId: $('#ProductBaseuomId').val(), Barcode: $('#ProductbarCodeId').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/ProductManagement/Updatesystemproductdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Product details has been updated.', 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Product details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savesystempricelistdata() {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
    var Priceliststations = new Array();
    checkboxes.forEach((checkbox) => {
        var Priceliststation = {};
        Priceliststation.StationId = checkbox.value;
        Priceliststations.push(Priceliststation);
    });
    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), PriceListname: $('#pricelistnameId').val(), ProductvariationId: $('#stationproductId').val(), ProductPrice: $('#stationpriceId').val(), ProductVat: $('#stationpricevatId').val(), PriceListprices: Priceliststations, Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    alert(JSON.stringify(uil));
    $.post("/PriceManagement/Addpricelistdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Price List details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Price List details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}
function savesystempricelistdetaildata() {
    var uil =
    {
        PriceListId: $('#SystemPriceListId').val(), PriceListname: $('#SystemPriceListnameId').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/PriceManagement/Editsystempricelistdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Price List details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}

function savesystempricelistpricedetaildata() {
    var uil =
    {
        PriceListId: $('#SystemPriceListId').val(), ProductvariationId: $('#stationproductId').val(), StationdataId: $('#systemstationId').val(), ProductPrice: $('#SystemPriceamountnameId').val(), ProductVat: $('#SystemVatvalueId').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/PriceManagement/Addpricelistpricenewdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Price List Price details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}


function savesystemdiscountslistdata() {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
    var Discountliststations = new Array();
    checkboxes.forEach((checkbox) => {
        var Discountliststation = {};
        Discountliststation.StationId = checkbox.value;
        Discountliststations.push(Discountliststation);
    });
    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), DiscountListname: $('#discountlistnameId').val(), ProductvariationId: $('#stationproductId').val(), ProductDiscountValue: $('#stationdiscountvalueId').val(), DiscountListStartTime: $('#discountliststarttimeId').val(),
        DiscountListDays: $('#discountlistdaysId').val().join(','), DiscountListEndTime: $('#discountlistendtimeId').val(), DiscountListpricestations: Discountliststations, CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/PriceManagement/Adddiscountlistdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Discount List details has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.respStatus == 1) {
            Swal.fire("Discount List details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}
function savesystemdiscountlistdetaildata() {
    var uil =
    {
        DiscountListId: $('#SystemDiscountListId').val(), DiscountListname: $('#SystemDiscountListnameId').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/PriceManagement/Editsystemdiscountlistdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Discount List details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }

    });
}
function savesystemdiscountlistvaluedetaildata() {
    var stationSelect = document.getElementById('systemstationId');
    var selectedOptions = stationSelect.selectedOptions;

    var selectedStations = [];
    for (var i = 0; i < selectedOptions.length; i++) {
        var stationId = selectedOptions[i].value;
        var stationObj = {
            StationId: parseInt(stationId) // Assuming StationId is a long in the model
        };
        selectedStations.push(stationObj);
    }

    var uil =
    {
        DiscountlistId: $('#SystemDiscountlistId').val(), ProductvariationId: $('#stationproductId').val(), Discountvalue: $('#stationdiscountvalueId').val(), Starttime: $('#discountliststarttimeId').val(),
        Endtime: $('#discountlistendtimeId').val(), DiscountListpricestations: selectedStations, Daysapplicable: $('#discountlistdaysId').val().join(','), Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/PriceManagement/Adddicountlistvaluenewdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModalLarge').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Discount List Values details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}


function savesystemequipmentdata() {
    if ($('#EquipmentRegistrationId').val() == '') {
        Swal.fire("Customer Equipment Not Created", 'Registration No#. is Required', "warning");
        return;
    }
    if ($('#EquipmentMakeId').val() == '' || $('#EquipmentMakeId').val() == 0) {
        Swal.fire("Customer Equipment Not Created", 'Equipment Make is Required', "warning");
        return;
    }
    if ($('#EquipmentModelId').val() == '' || $('#EquipmentModelId').val() == 0) {
        Swal.fire("Customer Equipment Not Created", 'Equipment Model is Required', "warning");
        return;
    }
    if ($('#EquipmentProductId').val() == '' || $('#EquipmentProductId').val() == 0) {
        Swal.fire("Customer Equipment Not Created", 'Equipment Fuel Type is Required', "warning");
        return;
    }
    if ($('#TankCapacityId').val() == '' || $('#TankCapacityId').val() == 0) {
        Swal.fire("Customer Equipment Not Created", 'Equipment Tank Capacity is Required', "warning");
        return;
    }
    if ($('#OdometerReadingId').val() == '') {
        Swal.fire("Customer Equipment Not Created", 'Odometer Reading is Required. Input 0 is Not Available', "warning");
        return;
    }

    var uil =
    {
        EquipmentId: $('#SystemEquipmentId').val(), AccountId: $('#EquipmentAccountId').val(), EquipmentReg: $('#EquipmentRegistrationId').val(), VehicleMakeId: $('#EquipmentMakeId').val(), VehicleModelId: $('#EquipmentModelId').val(),
        ProductVariationId: $('#EquipmentProductId').val(), TankCapacity: $('#TankCapacityId').val(), OdometerReading: $('#OdometerReadingId').val(), CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/AddCustomerAccountEquipmentdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Equipment details has been added.', 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Equipment details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemcustomeremployeedata() {
    if ($('#FirstnameId').val() == '') {
        Swal.fire("Customer Employee Not Created", 'Firstname is Required.', "warning");
        return;
    }
    if ($('#LastnameId').val() == '') {
        Swal.fire("Customer Employee Not Created", 'Lastname is Required.', "warning");
        return;
    }
    if ($('#EmailaddressId').val() == '') {
        Swal.fire("Customer Employee Not Created", 'Emailaddress is Required.', "warning");
        return;
    }
    var uil =
    {
        EmployeeId: $('#SystemEmployeeId').val(), AccountId: $('#EmployeeAccountId').val(), Firstname: $('#FirstnameId').val(), Lastname: $('#LastnameId').val(), Emailaddress: $('#EmailaddressId').val(),
        CreatedbyId: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/AddCustomerAccountEmployeedata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Employee details has been added.', 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Employee details Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountproductpolicy() {
    if ($('#SystemproductId').val() == '' || $('#SystemproductId').val() == 0) {
        Swal.fire("Account Product Policy Detail Not Created", 'Product is Required', "warning");
        return;
    }
    if ($('#ConsumptionLimitvalueId').val() == '') {
        Swal.fire("Account Product Policy Detail Not Created", 'Limit Value is Required', "warning");
        return;
    }
    if ($('#LimitperiodvalueId').val() == '' || $('#LimitperiodvalueId').val() == "0") {
        Swal.fire("Account Product Policy Detail Not Created", 'Limit Period is Required', "warning");
        return;
    }
    var uil =
    {
        AccountProductId: $('#AccountProductId').val(), AccountId: $('#PolicyAccountId').val(), ProductVariationId: $('#SystemproductId').val(), LimitValue: $('#ConsumptionLimitvalueId').val(), LimitPeriod: $('#LimitperiodvalueId').val(), Masknumber: $('#AccountmaskId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountproductpolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Account Product Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/Customeraccountpoliciesdetail?AccountId=" + parseInt(response.Data1) + "&Masknumber=" + response.Data2,
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Account Product Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountstationpolicy() {
    if ($('#SystemstationId').val() == '' || $('#SystemstationId').val() == 0) {
        Swal.fire("Account Station Policy Detail Not Created", 'Atleast One Station is Required', "warning");
        return;
    }
    var uil =
    {
        AccountStationId: $('#AccountStationId').val(), AccountId: $('#PolicyAccountId').val(), StationId: $('#SystemstationId').val(), Masknumber: $('#AccountmaskId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountstationpolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Account Station Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/Customeraccountpoliciesdetail?AccountId=" + parseInt(response.Data1) + "&Masknumber=" + response.Data2,
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Account Station Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountweekdaypolicy() {
    if ($('#AccountweekdayId').val() == '') {
        Swal.fire("Account Weekday Policy Detail Not Created", 'Account Weekday is Required', "warning");
        return;
    }
    if ($('#AccountStartDateId').val() == '') {
        Swal.fire("Account Weekday Policy Detail Not Created", 'Start time is Required', "warning");
        return;
    }
    if ($('#AccountEndDateId').val() == '') {
        Swal.fire("Account Weekday Policy Detail Not Created", 'Start time is Required', "warning");
        return;
    }
    var uil =
    {
        AccountWeekDaysId: $('#AccountWeekDaysId').val(), AccountId: $('#PolicyAccountId').val(), Masknumber: $('#AccountmaskId').val(), WeekDays: $('#AccountweekdayId').val().join(), StartTime: $('#AccountStartDateId').val(), EndTime: $('#AccountEndDateId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountweekdayypolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Account Week day Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/Customeraccountpoliciesdetail?AccountId=" + parseInt(response.Data1) + "&Masknumber=" + response.Data2,
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Account Week day Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountfrequencypolicy() {
    if ($('#AccountFrequencyCountId').val() == '' || $('#AccountFrequencyCountId').val() == 0) {
        Swal.fire("Account Frequency Policy Detail Not Created", 'Account Frequency is Required', "warning");
        return;
    }
    if ($('#AccountFrequencyPeriodId').val() == '' || $('#AccountFrequencyPeriodId').val() == '0') {
        Swal.fire("Account Frequency Policy Detail Not Created", 'Account Frequency Period is Required', "warning");
        return;
    }
    var uil =
    {
        AccountFrequencyId: $('#AccountFrequencyId').val(), AccountId: $('#PolicyAccountId').val(), Masknumber: $('#AccountmaskId').val(), Frequency: $('#AccountFrequencyCountId').val(), FrequencyPeriod: $('#AccountFrequencyPeriodId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountfrequencypolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Account Frequency Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/Customeraccountpoliciesdetail?AccountId=" + parseInt(response.Data1) + "&Masknumber=" + response.Data2,
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Account Frequency Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function permanentdeletestationthis(event) {
    Swal.fire({
        title: "Are you sure you want to Remove this?",
        text: "Once Removed, Cannot be Recovered!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Remove it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var removeBtn = event.target;
            var Tablename = removeBtn.getAttribute('data-Tablename');
            var Columnidname = removeBtn.getAttribute('data-Columnidname');
            var Entryid = removeBtn.getAttribute('data-Entryid');
            var AccountId = removeBtn.getAttribute('data-AccountId');
            var Masknumber = removeBtn.getAttribute('data-Masknumber');
            var uil =
            {
                Tablename: Tablename, Columnidname: Columnidname, Entryid: Entryid,
                CreatedbyId: $('#systemLoggedinedUserId').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserId').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
                Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            $.post("/CustomerManagement/AllRemoveDeativateActions", uil, function (response) {
                if (response.RespStatus == 0) {
                    Swal.fire('Item Removed!', 'Good.', 'success')
                    $('#FuelcardsystemModalLarge').hide();
                    $.ajax({
                        url: "/CustomerManagement/Customeraccountpoliciesdetail?AccountId=" + parseInt(AccountId) + "&Masknumber=" + Masknumber,
                        type: "GET",
                        success: function (result) {
                            $(".modal-content").html(result);
                            $("#FuelcardsystemModalExtraLarge").modal("show");
                        }
                    });
                } else if (response.RespStatus == 1) {
                    Swal.fire("Oops!", response.RespMessage, "warning");
                }
                else {
                    Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
                }

            });
        } else {
            Swal.fire("Thank you!", "Remove once you are sure", "info");
        }
    });
};

function permanentdeleteemployeethis(event) {
    Swal.fire({
        title: "Are you sure you want to Remove this?",
        text: "Once Removed, Cannot be Recovered!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Remove it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var removeBtn = event.target;
            var Tablename = removeBtn.getAttribute('data-Tablename');
            var Columnidname = removeBtn.getAttribute('data-Columnidname');
            var Entryid = removeBtn.getAttribute('data-Entryid');
            var EmployeeId = removeBtn.getAttribute('data-EmployeeId');
            var EmployeeName = removeBtn.getAttribute('data-EmployeeName');
            var uil =
            {
                Tablename: Tablename, Columnidname: Columnidname, Entryid: Entryid,
                CreatedbyId: $('#systemLoggedinedUserId').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserId').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
                Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            $.post("/CustomerManagement/AllRemoveDeativateActions", uil, function (response) {
                if (response.RespStatus == 0) {
                    Swal.fire('Item Removed!', 'Good.', 'success')
                    $('#FuelcardsystemModalLarge').hide();
                    $.ajax({
                        url: "/CustomerManagement/CustomerAccountEmployeePolicies?EmployeeId=" + parseInt(EmployeeId),
                        type: "GET",
                        success: function (result) {
                            $(".modal-content").html(result);
                            $("#FuelcardsystemModalExtraLarge").modal("show");
                        }
                    });
                } else if (response.RespStatus == 1) {
                    Swal.fire("Oops!", response.RespMessage, "warning");
                }
                else {
                    Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
                }

            });
        } else {
            Swal.fire("Thank you!", "Remove once you are sure", "info");
        }
    });
};

function permanentdeleteequipmentthis(event) {
    Swal.fire({
        title: "Are you sure you want to Remove this?",
        text: "Once Removed, Cannot be Recovered!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Remove it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var removeBtn = event.target;
            var Tablename = removeBtn.getAttribute('data-Tablename');
            var Columnidname = removeBtn.getAttribute('data-Columnidname');
            var Entryid = removeBtn.getAttribute('data-Entryid');
            var EquipmentId = removeBtn.getAttribute('data-EquipmentId');
            var uil =
            {
                Tablename: Tablename, Columnidname: Columnidname, Entryid: Entryid,
                CreatedbyId: $('#systemLoggedinedUserId').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedId: $('#systemLoggedinedUserId').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
                Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
            };
            $.post("/CustomerManagement/AllRemoveDeativateActions", uil, function (response) {
                if (response.RespStatus == 0) {
                    Swal.fire('Item Removed!', 'Good.', 'success')
                    $('#FuelcardsystemModalLarge').hide();
                    $.ajax({
                        url: "/CustomerManagement/CustomerAccountEquipmentPolicies?EquipmentId=" + parseInt(EquipmentId),
                        type: "GET",
                        success: function (result) {
                            $(".modal-content").html(result);
                            $("#FuelcardsystemModalExtraLarge").modal("show");
                        }
                    });
                } else if (response.RespStatus == 1) {
                    Swal.fire("Oops!", response.RespMessage, "warning");
                }
                else {
                    Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
                }

            });
        } else {
            Swal.fire("Thank you!", "Remove once you are sure", "info");
        }
    });
};

function savecustomeraccountemployeeproductpolicy() {
    if ($('#SystemEmployeeproductId').val() == '' || $('#SystemEmployeeproductId').val() == 0) {
        Swal.fire("Employee Product Policy Detail Not Created", 'Product is Required', "warning");
        return;
    }
    if ($('#EmployeeConsumptionLimitvalueId').val() == '') {
        Swal.fire("Employee Product Policy Detail Not Created", 'Limit Value is Required', "warning");
        return;
    }
    if ($('#EmployeeLimitperiodvalueId').val() == '' || $('#EmployeeLimitperiodvalueId').val() == "0") {
        Swal.fire("Employee Product Policy Detail Not Created", 'Limit Period is Required', "warning");
        return;
    }
    var uil =
    {
        EmployeeProductId: $('#EmployeeProductId').val(), EmployeeId: $('#PolicyEmployeeId').val(), ProductVariationId: $('#SystemEmployeeproductId').val(), LimitValue: $('#EmployeeConsumptionLimitvalueId').val(), LimitPeriod: $('#EmployeeLimitperiodvalueId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountemployeeproductpolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Employee Product Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEmployeePolicies?EmployeeId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Employee Product Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountemployeestationpolicy() {
    var uil =
    {
        EmployeeStationId: $('#EmployeeStationId').val(), EmployeeId: $('#PolicyEmployeeId').val(), StationId: $('#SystemEmployeestationId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountemployeestationpolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Employee Station Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEmployeePolicies?EmployeeId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire(" Employee Station Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountemployeeweekdaypolicy() {
    if ($('#EmployeeweekdayId').val() == '') {
        Swal.fire("Emloyee Weekday Policy Detail Not Created", ' Weekday is Required', "warning");
        return;
    }
    if ($('#EmployeeStartDateId').val() == '') {
        Swal.fire("Emloyee Weekday Policy Detail Not Created", 'Start time is Required', "warning");
        return;
    }
    if ($('#EmployeeEndDateId').val() == '') {
        Swal.fire("Emloyee Weekday Policy Detail Not Created", 'Start time is Required', "warning");
        return;
    }
    var uil =
    {
        EmployeeWeekDaysId: $('#EmployeeWeekDaysId').val(), EmployeeId: $('#PolicyEmployeeId').val(), WeekDays: $('#EmployeeweekdayId').val().join(), StartTime: $('#EmployeeStartDateId').val(), EndTime: $('#EmployeeEndDateId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountemployeeweekdaypolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Employee Week day Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEmployeePolicies?EmployeeId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Employee Week day Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountemployeefrequencypolicy() {
    if ($('#EmployeeFrequencyCountId').val() == '' || $('#EmployeeFrequencyCountId').val() == 0) {
        Swal.fire("Employee Frequency Policy Detail Not Created", 'Employee Frequency is Required', "warning");
        return;
    }
    if ($('#EmployeeFrequencyPeriodId').val() == '' || $('#EmployeeFrequencyPeriodId').val() == '0') {
        Swal.fire("Employee Frequency Policy Detail Not Created", 'Employee Frequency Period is Required', "warning");
        return;
    }
    var uil =
    {
        EmployeeFrequencyId: $('#EmployeeFrequencyId').val(), EmployeeId: $('#PolicyEmployeeId').val(), Frequency: $('#EmployeeFrequencyCountId').val(), FrequencyPeriod: $('#EmployeeFrequencyPeriodId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountemployeefrequencypolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Employee Frequency Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEmployeePolicies?EmployeeId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Employee Frequency Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}



function savecustomeraccountequipmentproductpolicy() {
    if ($('#SystemEquipmentproductId').val() == '' || $('#SystemEquipmentproductId').val() == 0) {
        Swal.fire("Equipment Product Policy Detail Not Created", 'Product is Required', "warning");
        return;
    }
    if ($('#EquipmentConsumptionLimitvalueId').val() == '') {
        Swal.fire("Equipment Product Policy Detail Not Created", 'Limit Value is Required', "warning");
        return;
    }
    if ($('#EquipmentLimitperiodvalueId').val() == '' || $('#EquipmentLimitperiodvalueId').val() == "0") {
        Swal.fire("Equipment Product Policy Detail Not Created", 'Limit Period is Required', "warning");
        return;
    }
    var uil =
    {
        EquipmentProductId: $('#EquipmentProductId').val(), EquipmentId: $('#PolicyEquipmentId').val(), ProductVariationId: $('#SystemEquipmentproductId').val(), LimitValue: $('#EquipmentConsumptionLimitvalueId').val(), LimitPeriod: $('#EquipmentLimitperiodvalueId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountequipmentproductpolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Equipment Product Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEquipmentPolicies?EquipmentId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Equipment Product Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountequipmentstationpolicy() {
    var uil =
    {
        EquipmentStationId: $('#EquipmentStationId').val(), EquipmentId: $('#PolicyEquipmentId').val(), StationId: $('#SystemEquipmentstationId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountequipmentstationpolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Equipment Station Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEquipmentPolicies?EquipmentId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Equipment Station Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountequipmentweekdaypolicy() {
    if ($('#EquipmentweekdayId').val() == '') {
        Swal.fire("Equipment Weekday Policy Detail Not Created", ' Weekday is Required', "warning");
        return;
    }
    if ($('#EquipmentStartDateId').val() == '') {
        Swal.fire("Equipment Weekday Policy Detail Not Created", 'Start time is Required', "warning");
        return;
    }
    if ($('#EquipmentEndDateId').val() == '') {
        Swal.fire("Equipment Weekday Policy Detail Not Created", 'Start time is Required', "warning");
        return;
    }
    var uil =
    {
        EquipmentWeekDaysId: $('#EquipmentWeekDaysId').val(), EquipmentId: $('#PolicyEquipmentId').val(), WeekDays: $('#EquipmentweekdayId').val().join(), StartTime: $('#EquipmentStartDateId').val(), EndTime: $('#EquipmentEndDateId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountequipmentweekdaypolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Equipment Week day Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEquipmentPolicies?EquipmentId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Equipment Week day Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savecustomeraccountequipmentfrequencypolicy() {
    if ($('#EquipmentFrequencyCountId').val() == '' || $('#EquipmentFrequencyCountId').val() == 0) {
        Swal.fire("Equipment Frequency Policy Detail Not Created", 'Equipment Frequency is Required', "warning");
        return;
    }
    if ($('#EquipmentFrequencyPeriodId').val() == '' || $('#EquipmentFrequencyPeriodId').val() == '0') {
        Swal.fire("Equipment Frequency Policy Detail Not Created", 'Equipment Frequency Period is Required', "warning");
        return;
    }
    var uil =
    {
        EquipmentFrequencyId: $('#EquipmentFrequencyId').val(), EquipmentId: $('#PolicyEquipmentId').val(), Frequency: $('#EquipmentFrequencyCountId').val(), FrequencyPeriod: $('#EquipmentFrequencyPeriodId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), Createdby: $('#systemLoggedinedUserNameId').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserNameId').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/CustomerManagement/Addcustomeraccountequipmentfrequencypolicydata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', 'Equipment Frequency Policy has been added.', 'success')
            $('#FuelcardsystemModalLarge').hide();
            $.ajax({
                url: "/CustomerManagement/CustomerAccountEquipmentPolicies?EquipmentId=" + parseInt(response.Data1),
                type: "GET",
                success: function (result) {
                    $(".modal-content").html(result);
                    $("#FuelcardsystemModalExtraLarge").modal("show");
                }
            });

        } else if (response.RespStatus == 1) {
            Swal.fire("Equipment Frequency Policy Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}

function savesystemtenantgadgetdata() {
    var uil =
    {
        GadgetId: $('#SystemGadgetId').val(), GadgetName: $('#GadgetnameId').val(), Descriptions: $('#GadgetDescriptionId').val(),
        Imei1: $('#GadgetEmei1Id').val(), Imei12: $('#GadgetEmei2Id').val(), Serialnumber: $('#GadgetSerialnumberId').val(), StationId: $('#GadgetStationId').val(),
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), TenantId: $('#systemLoggedinedTenantid').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/HardwareManagement/AddTenantGadgetdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Tenant Gadget Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}


function savesystemtenantcarddata() {
    document.getElementById("saveSystemCardId").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";

    var uil =
    {
        TenantId: $('#systemLoggedinedTenantid').val(), CardId: $('#SystemCardId').val(), CardSNO: $('#CardSNOId').val(), CardUID: $('#CardUIDId').val(), TagtypeId: $('#CardTypeId').val(),
        CreatedBy: $('#systemLoggedinedUserid').val(), ModifiedBy: $('#systemLoggedinedUserid').val(), TenantId: $('#systemLoggedinedTenantid').val(),
        DateCreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), DateModified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/HardwareManagement/AddTenantCarddata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Tenant Card Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
    });
}
function savesystemrolesdetails() {
    document.getElementById("saveSystemRolesBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";
    var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked:not(#staffRolesCheckAll)');
    var Perms = new Array();
    checkboxes.forEach((checkbox) => {
        var Permission = {};
        Permission.PermissionId = checkbox.value;
        Perms.push(Permission);
    });
    if ($('#RolenameId').val() == '') {
        Swal.fire("Staff Role Not Created", 'Role Name is Required', "warning");
        document.getElementById("saveSystemRolesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#DescriptionId').val() == '') {
        Swal.fire("Staff Role Not Created", 'Role Description is Required', "warning");
        document.getElementById("saveSystemRolesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if (Perms.length < 1) {
        Swal.fire("Staff Role Not Created", 'Atleast one Permission is Required', "warning");
        document.getElementById("saveSystemRolesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }

    var uil = {
        TenantId: $('#systemLoggedinedTenantid').val(), RoleId: $('#RoleIdValueId').val(), Rolename: $('#RolenameId').val(), Roledescription: $('#DescriptionId').val(), Permissions: Perms,
        Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(), Tenantid: $('#systemLoggedinedTenantid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/StaffManagement/Addsystemroledata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire('Saved!', response.RespMessage, 'success')
            $('#FuelcardsystemModal').hide();
            setTimeout(function () { location.reload(); }, 1000);
        } else if (response.RespStatus == 1) {
            Swal.fire("Role Not saved", response.RespMessage, "warning");
        }
        else {
            Swal.fire("Oops! Something Went Wrong", "Database Error has occured. Kindly Contact our support team.", "error");
        }
        document.getElementById("saveSystemRolesBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}


function savesystemstaffdetails() {
    document.getElementById("saveSystemStaffBtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Processing...";

    var domain = $('#basic-addon2').val();
    var username = $('#UsernameId').val();

    var checkboxes = document.querySelectorAll('input[name="Systemstationsdata"]:checked');
    var Stations = new Array();
    checkboxes.forEach((checkbox) => {
        var Station = {};
        Station.StationId = checkbox.value;
        Stations.push(Station);
    });

    if ($('#FirstnameId').val() == '') {
        Swal.fire("Staff Detail Not Created", 'Firstname is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#LastnameId').val() == '') {
        Swal.fire("Staff Detail Not Created", 'Lastname is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#UsernameId').val() == '') {
        Swal.fire("Staff Detail Not Created", 'Username is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#EmailAddressId').val() == '') {
        Swal.fire("Staff Detail Not Created", 'Emailaddress is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#inputGroup-sizing-sm').val() == '' || $('#inputGroup-sizing-sm').val() == 0) {
        Swal.fire("Staff Detail Not Created", 'Phonenumber Prefix is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#PhonenumberId').val() == '') {
        Swal.fire("Staff Detail Not Created", 'Phonenumber is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if ($('#StaffRoleId').val() == '' || $('#StaffRoleId').val() == 0) {
        Swal.fire("Staff Detail Not Created", 'Role is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    if (Stations.length < 1) {
        Swal.fire("Staff Detail Not Created", 'Atleast one Station is Required', "warning");
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
        return;
    }
    var uil = {
        Userid: $('#UserId').val(), Firstname: $('#FirstnameId').val(), Lastname: $('#LastnameId').val(), Emailaddress: $('#EmailAddressId').val(), Username: username + '' + domain, Phoneid: $('#inputGroup-sizing-sm').val(), Phonenumber: $('#PhonenumberId').val(),
        Roleid: $('#StaffRoleId').val(), LimitTypeId: $('#LimittypeId').val(), LimitTypeValue: $('#TopupLimitvalueId').val(), SystemStation: Stations, Createdby: $('#systemLoggedinedUserid').val(), Modifiedby: $('#systemLoggedinedUserid').val(),
        Datecreated: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), Datemodified: new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '),
    };
    $.post("/StaffManagement/Addsystemstaffdata", uil, function (response) {
        if (response.RespStatus == 0) {
            Swal.fire("Staff Detail", "Staff has been added and email sent.", "success");
            setTimeout(function () { location.reload(); }, 500);
        } else {
            Swal.fire("Staff Detail Not Created", response.RespMessage, "error");
        }
        document.getElementById("saveSystemStaffBtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "SAVE";
    });
}


function Getsystemcardswiththistype(cardtype) {
    var onj = "<option value=''>Select Model</option>";
    if (cardtype === "") { $('#SystemcardmaskId').html(onj); return; }
    if (cardtype === "1") {
        $('#SystemcardmaskId').hide();
        $('.select2').hide();
        $('#SystemcardvitualmaskId').show();
        $('#SystemcardmaskId').val(1);
        return;
    } else {
        $('.select2').show();
        $('#SystemcardvitualmaskId').hide();
        $.get("/CustomerManagement/GetsystemcardDropDownDatawiththistype/" + cardtype, null, function (data) {
            $.each(data, function (a, x) { onj = onj + "<option value='" + x.Value + "'>" + x.Text + "</option>"; })
            $('#SystemcardmaskId').html(onj);
        });
    }

}