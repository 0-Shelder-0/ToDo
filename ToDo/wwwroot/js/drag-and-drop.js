function onDragStart(event) {
    event.dataTransfer.effectAllowed = 'move';
    event.dataTransfer.setData("text", event.target.getAttribute('id'));
}

function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event) {
    let el;
    let targetId;
    let recordId = event.dataTransfer.getData("text");

    try {
        targetId = getId(event.target.getAttribute('id'));
        el = $(`#record-padding-${targetId}`);
    } catch (e) {
        el = $(event.target);
    }

    let data = {};
    data.NewColumnId = getParentId(el);
    data.RecordId = getId(recordId);
    data.AdjacentRecordId = targetId;

    moveRecord(data).then(() => window.location.reload());
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

function getId(elem) {
    let id = elem.split('-');
    return id[id.length - 1];
}

function getParentId(elem) {
    let id = elem.parent().attr('id');
    return getId(id);
}