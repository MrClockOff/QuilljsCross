/* Initialisation */
var quillEditorElement = document.getElementById('quill-editor');
var quill = new Quill(quillEditorElement, {
    theme: 'snow',
    modules: {
        toolbar: false
    }
});

observe(quillEditorElement, 'offsetHeight', function (newValue, oldValue) {
    var messageArgs = {
        newValue: newValue,
        oldValue: oldValue
    };

    window.webkit.messageHandlers.onContentResizedMessage.postMessage(messageArgs);
});

/* Handlers */
function setHtml(text) {
    quill.clipboard.dangerouslyPasteHTML(text);
}

function setPlaceholder(text) {
    quill.root.dataset.placeholder = text;
}

/**
 * Format selection as a list
 * @param {any} formattingAttributeValue
 * @param {any} apply
 */
function setList(formattingAttributeValue, apply) {
    setLineFormat('list', formattingAttributeValue, apply);
}

/**
 * Format selected text as bold, italic or underline
 * @param {any} formattingAttributeKey
 * @param {any} apply
 */
function setFormat(formattingAttributeKey, apply) {
    setTextFormat(formattingAttributeKey, apply);
}

/**
 * Set selected text alignment
 * @param {any} formattingAttributeValue
 */
function setAlignment(formattingAttributeValue) {
    setLineFormat('align', formattingAttributeValue, true);
}

/**
 * Set selected text format
 * @param {any} formattingAttributeKey
 * @param {any} formattingAttributeValue
 */
function setTextFormat(formattingAttributeKey, formattingAttributeValue) {
    var range = quill.getSelection();

    if (!range) {
        console.log('Cursor is not in editor');
        return;
    }

    quill.formatText(range.index, range.length, formattingAttributeKey, formattingAttributeValue);
}

/**
 * Set selected line format
 * @param {any} formattingAttributeKey
 * @param {any} formattingAttributeValue
 * @param {any} apply
 */
function setLineFormat(formattingAttributeKey, formattingAttributeValue, apply) {
    var range = quill.getSelection();

    if (!range) {
        console.log('Cursor is not in editor');
        return;
    }

    if (apply) {
        quill.formatLine(range.index, range.length, formattingAttributeKey, formattingAttributeValue);
        return;
    }

    quill.formatLine(range.index, range.length, formattingAttributeKey, apply);
}

/**
 * Observe object property changes
 * @param {any} object
 * @param {any} property
 * @param {any} callback
 */
function observe(object, property, callback) {
    var oldValue = null;

    setInterval(function () {
        var newValue = object[property];

        if (oldValue == newValue) {
            return;
        }

        oldValueArg = oldValue;
        oldValue = newValue;
        callback(newValue, oldValueArg);
    }, 100);
}

/* Event listeners */
quill.on('selection-change', function (range) {
    console.log('selection-change', range);

    if (!range) {
        return;
    }

    var formats = quill.getFormat();
    var formattingAttributes = [];

    for (format in formats) {
        if (format == 'list') {
            formattingAttributes.push(formats[format]);
        } else if (format == 'align') {
            formattingAttributes.push(formats[format]);
        } else {
            formattingAttributes.push(format);
        }
    }

    var messageArgs = {
        text: quill.getText(range.index, range.length),
        index: range.index,
        length: range.length,
        formattingAttributes: formattingAttributes.join(',')
    }

    window.webkit.messageHandlers.onTextSelectedInRangeMessage.postMessage(messageArgs);
});

quill.on('text-change', function (delta, oldDelta, source) {
    var messageArgs = {
        html: quill.root.innerHTML
    };

    window.webkit.messageHandlers.onTextChangedMessage.postMessage(messageArgs);
});