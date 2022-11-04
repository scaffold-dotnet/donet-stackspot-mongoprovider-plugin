import shutil
import os
from templateframework.metadata import Metadata

def run(metadata: Metadata = None):    
    try:
        src_folder = os.path.join(metadata.target_path, "src")
        isExist = os.path.exists(src_folder)

        if isExist:
            print("Path to delete: " + src_folder)
            shutil.rmtree(src_folder)
    except OSError as e:  # if failed, report it back to the user ##
        print("Error: %s - %s." % (e.filename, e.strerror))

    return metadata
