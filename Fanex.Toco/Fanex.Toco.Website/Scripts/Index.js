/* File Created: June 4, 2015 */
function LoadProduct(productUrl) {
    var url = productUrl;
    document.getElementById('mainFrame').setAttribute('src', resolveClientUrl(url));
}

function LoadEnableStatusProduct(select) {
    var url = 'customersetting/enablestatus/productstatus?custid=1176086&uplineid=1176083&userid=8&productid={0}&level=2&enable=true&enabledownlines=false&hasdownline=true&username=agent01&enableuplines=true';
    if (select.value == 'Casino') {
        url = url + '&synced=true';
    }

    if (select.value == 'Casino') {
        url = url + '&isCasino=true';
    }

    document.getElementById('mainFrame').src = url.replace('{0}', select.value);
}