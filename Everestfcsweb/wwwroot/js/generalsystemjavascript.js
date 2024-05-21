function LoadCustomerAgreementForm() {
    var e = document.getElementById("CustomerAgreementTypeNameId");
    document.getElementById('AgreementTypeId').value = e.value;
    if (e.value = 'Credit') {
        $("#genall").show();
        $("#crdtall").show();
    }else
    {
        $("#genall").hide();
        $("#crdtall").hide();
    }
}


