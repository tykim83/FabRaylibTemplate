export function PickFile() {
    return new Promise((resolve, reject) => {
        var input = document.createElement('input');
        input.type = 'file';

        input.onchange = function (e) {
            var file = e.target.files[0];
            console.log("JS: File picked:", file);

            if (file) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    resolve({
                        name: file.name,
                        content: new Uint8Array(event.target.result)
                    });
                };
                reader.readAsArrayBuffer(file);
            } else {
                reject(new Error("No file selected"));
            }
        };

        input.click();
    });
}

export function DownloadFile(fileName, dataBase64) {
    const byteCharacters = atob(dataBase64);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);

    const blob = new Blob([byteArray], { type: "image/png" });
    const url = URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();

    document.body.removeChild(a);
    URL.revokeObjectURL(url);
}
