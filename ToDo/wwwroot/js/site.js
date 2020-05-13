function onDragStart(event) {
    event.dataTransfer.effectAllowed = 'move';
    event.dataTransfer.setData("text", event.target.getAttribute('id'));
}

function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event) {
    let element = document.getElementById(event.dataTransfer.getData("text"));
    let targetId = event.target.getAttribute('id');

    if (element != null && targetId.match('record'))
        event.target.parentElement.append(element);
    else if (element != null && targetId.match('column'))
        event.target.append(element);

    event.stopPropagation();
}