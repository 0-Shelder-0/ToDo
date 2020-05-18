function onDragStart(event) {
    event.dataTransfer.effectAllowed = 'move';
    event.dataTransfer.setData("text", event.target.getAttribute('id'));
}

function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event) {
    let recordId = event.dataTransfer.getData("text").split('-')[1];
    let columnId = event.target.getAttribute('id').split('-')[1];
    let url = document.URL.split('/');

    let data = {};
    data.RecordId = recordId;
    data.NewColumnId = columnId;
    data.BoardId = url[url.length - 1];

    
    $.ajax({
        type: "POST",
        url: "Board/MoveRecord",
        data: data,
    });
}