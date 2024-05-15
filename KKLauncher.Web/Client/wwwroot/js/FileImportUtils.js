export var GLOBAL = {};
GLOBAL.DotNetReference = null;
GLOBAL.SetDotNetReference = function (pDotNetReference) {
    GLOBAL.DotNetReference = pDotNetReference;
};

export function InvokeImport() {
    var element = document.getElementById("import-btn");
    if (element) {
        element.click()
    }
}
