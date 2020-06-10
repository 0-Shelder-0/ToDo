let recordId;

function onDragStart(event, id) {
    event.dataTransfer.effectAllowed = 'move';
    recordId = id;
}

function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event, columnId) {
    let data = {};
    data.NewColumnId = columnId;
    data.RecordId = recordId;

    moveRecord(data).then(() => window.location.reload());
    recordId = null;
}

async function moveRecord(data) {
    $.ajax({
        type: "POST",
        url: "Board/MoveRecord",
        data: data
    });
    await sleep(50);
}

async function sleep(msec) {
    return new Promise(resolve => setTimeout(resolve, msec));
}