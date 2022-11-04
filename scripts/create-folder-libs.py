import shutil
import os
from templateframework.metadata import Metadata

def run(metadata: Metadata = None):
    try:
        lib_path =  os.path.join(metadata.component_path, "templates", "src", "output", "MongoProvider.dll") 
        libs_folder = os.path.join(metadata.target_path, "libs")
        isExist = os.path.exists(libs_folder)

        if isExist == False:
            print("Path to create: " + libs_folder)
            mode = 0o666
            os.mkdir(libs_folder, mode)
            
        shutil.copy(lib_path, libs_folder)
        
    except OSError as e:  # if failed, report it back to the user ##
        print("Error: %s - %s." % (e.filename, e.strerror))

    return metadata
