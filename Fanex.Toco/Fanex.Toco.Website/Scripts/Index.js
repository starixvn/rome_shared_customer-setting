/* File Created: June 4, 2015 */
function LoadProduct(productUrl) {
    var url = productUrl;
    document.getElementById('mainFrame').setAttribute('src', resolveClientUrl(url));
}