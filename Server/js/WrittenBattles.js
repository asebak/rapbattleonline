function limitwritten(textarea) {
    var lines = textarea.value.split("\n");
    for (var i = 0; i < lines.length; i++) {
        if (lines[i].length <= length) continue;
        var j = 0;
        space = length;
        while (j++ <= length) {
            if (lines[i].charAt(j) === " ") {
                space = j;
            }
        }
        lines[i + 1] = lines[i].substring(space + 1) + (lines[i + 1] || "");
        lines[i] = lines[i].substring(0, space);
    }
    textarea.value = lines.slice(0, barLength).join("\n");
}