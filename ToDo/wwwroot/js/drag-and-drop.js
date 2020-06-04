let recordId;

function onDragStart(event) {
    event.dataTransfer.effectAllowed = 'move';
    recordId = event.target.getAttribute('id');
}

function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event) {
    let elem;

    try {
        let targetId = getId(event.target.getAttribute('id'));
        elem = $(`#record-padding-${targetId}`);
    } catch (e) {
        elem = $(event.target);
    }

    let data = {};
    data.NewColumnId = getParentId(elem);
    data.RecordId = getId(recordId);

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

function getId(elem) {
    let id = elem.split('-');
    return id[id.length - 1];
}

function getParentId(elem) {
    let id = elem.parent().attr('id');
    return getId(id);
}