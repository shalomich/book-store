import React, { useState} from 'react';

export const getFormData = (data) => {
    let form = {}
    let formCollection = Array.prototype.slice.call(data)
    formCollection
        .filter(formEl=>formEl.nodeName ==='DIV')
        .forEach(div => {
            let divEls = Array.prototype.slice.call(div.children)
            console.log(divEls)
            divEls.forEach(el => {
                if (el.nodeName !== 'SPAN' && el.nodeName !== 'SELECT' && el.type !== 'file' && (el.value !== ''|| el.value !== 0)){
                    form[el.name] = el.value
                }
                else if (el.nodeName === 'SELECT') {
                    if (!el.attributes.multiple) {
                        form[el.name] = el.value
                    }
                    else {
                        let options = []
                        Array.prototype.slice.call(el.options).forEach(option => {
                            if (option.selected) {
                                options.push(option.value)
                            }
                        })
                        form[el.name] = options
                    }
                }
                else if (el.type === 'file') {
                    console.log(GetImages(Array.prototype.slice.call(el.files)))
                    form[el.name] = GetImages(Array.prototype.slice.call(el.files))
                }
            })
        })
    console.log(form)
    return form
}

const base64ToObject = (name, data) => {
    data = data.split(':')[1]
    let format = data.split(';')[0]
    data = data.split(';')[1]
    let encoding = data.split(',')[0]
    data = data.split(',')[1]
    return  {
        name: name,
        format: format,
        encoding: encoding,
        data: data
    }
}

const GetImages = (files) => {
    let images = []
    files.forEach((file, index) => {
        let name = file.name.replace(/\..*/, '')
        let image = handleUpload(file).then(response => {
            return base64ToObject(name, response)
        })
            .then()
        console.log(image)
        images.push(image)
    })
    return images
}

const handleUpload = async (file) => {
    try {
        return await readUploadedFileAsURL(file)
    } catch (e) {
        console.warn(e.message)
    }
}

const readUploadedFileAsURL = (inputFile) => {
    const temporaryFileReader = new FileReader();

    return new Promise((resolve, reject) => {
        temporaryFileReader.onerror = () => {
            temporaryFileReader.abort();
            reject(new DOMException("Problem parsing input file."));
        };

        temporaryFileReader.onload = () => {
            resolve(temporaryFileReader.result);
        };
        temporaryFileReader.readAsDataURL(inputFile);
    });
};
