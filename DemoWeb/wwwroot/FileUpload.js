﻿const readUploadedFileAsText = (inputFile) => {
    const temporaryFileReader = new FileReader();
    return new Promise((resolve, reject) => {
        temporaryFileReader.onerror = () => {
            temporaryFileReader.abort();
            reject(new DOMException("Problem parsing input file."));
        };
        temporaryFileReader.addEventListener("load", function () {
            var data = {
                content: temporaryFileReader.result.split(',')[1]
            };
            resolve(data);
        }, false);
        temporaryFileReader.readAsDataURL(inputFile.files[0]);
    });
};
Blazor.registerFunction("getFileData", function (inputFile) {

    return readUploadedFileAsText(inputFile);
});